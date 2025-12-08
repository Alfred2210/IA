using UnityEngine;
using System.Collections.Generic;



public class Visualizer : MonoBehaviour
{
    private MainDll main;
    public float pointSize = 0.1f;

    private List<GameObject> points = new List<GameObject>();
    private Color blue = Color.blue;
    private Color red = Color.red;

    public void Initialize(MainDll main)
    {
        this.main = main;
        if (Camera.main != null)
        {
            mainCamera = Camera.main;
        }
    }

    public Camera mainCamera;

    public void SetCamera2D()
    {
        if (mainCamera == null) return;
        mainCamera.orthographic = true;
        mainCamera.orthographicSize = 5f;
        mainCamera.transform.position = new Vector3(0f, 0f, -10f);
        mainCamera.transform.rotation = Quaternion.identity;
    }

    public void SetCamera3D()
    {
        if (mainCamera == null) return;
        mainCamera.orthographic = false;
        mainCamera.fieldOfView = 60f;
        
        mainCamera.transform.position = new Vector3(5f, 5f, -5f);
        mainCamera.transform.LookAt(new Vector3(1.5f, 2.5f, 1.5f)); 
    }

    public void CreatePoint(float x, float y, Color color)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.position = new Vector3(x, y, -2f);
        quad.transform.localScale = Vector3.one * pointSize;

        Renderer r = quad.GetComponent<Renderer>();
        r.material.color = color;

        points.Add(quad);
    }
    public void CreatePoint(float x, float y, float z, Color color)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, z, y); 
        sphere.transform.localScale = Vector3.one * pointSize;
        Renderer r = sphere.GetComponent<Renderer>();
        r.material.color = color;
        points.Add(sphere);
    }

    // Classification
    public void DrawLimitsClassification(float xMin, float xMax, float yMin, float yMax)
    {
        for (int i = 0; i <= 30; i++)
        {
            for (int j = 0; j <= 30; j++)
            {
                float x = Mathf.Lerp(xMin, xMax, i / 30f);
                float y = Mathf.Lerp(yMin, yMax, j / 30f);

                double[,] test = { { x, y } };
                double[] pred = main.PredictClassificationModel(test);

                // afficher les predictions
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = new Vector3(x, y, 0.1f);
                quad.transform.localScale = Vector3.one * pointSize * 1.0f;

                // couleur selon la classe
                Color color = pred[0] > 0 ? new Color(0.5f, 0.5f, 1f, 0.3f) : new Color(1f, 0.5f, 0.5f, 0.3f);
                quad.GetComponent<Renderer>().material.color = color;
                points.Add(quad);
            }
        }
    }

    // Regression
    public void DrawLimitsRegression(float xMin, float xMax, float yMin, float yMax)
    {
        int resolution = 20;
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        int[] triangles = new int[resolution * resolution * 6];
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i <= resolution; i++)
        {
            for (int j = 0; j <= resolution; j++)
            {
                float x = Mathf.Lerp(xMin, xMax, i / (float)resolution);
                float y = Mathf.Lerp(yMin, yMax, j / (float)resolution);
                
                double[,] test = { { x, y } };
                double[] pred = main.PredictRegressionModel(test);
                float z = (float)pred[0];

                int index = i * (resolution + 1) + j;
                vertices[index] = new Vector3(x, z, y);
                float t = Mathf.InverseLerp(0f, 5f, z);
                colors[index] = Color.Lerp(new Color(0f, 0f, 0f, 0.5f), new Color(0f, 1f, 0f, 0.5f), t); 
            }
        }

        int triIndex = 0;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                int topLeft = i * (resolution + 1) + j;
                int topRight = topLeft + 1;
                int bottomLeft = (i + 1) * (resolution + 1) + j;
                int bottomRight = bottomLeft + 1;

                // First triangle
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = topRight;

                // Second triangle
                triangles[triIndex++] = topRight;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = bottomRight;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();

        GameObject plane = new GameObject("RegressionPlane");
        MeshFilter mf = plane.AddComponent<MeshFilter>();
        MeshRenderer mr = plane.AddComponent<MeshRenderer>();
        mf.mesh = mesh;

        
        Material mat = new Material(Shader.Find("Mobile/Particles/Alpha Blended"));
        mr.material = mat;

        points.Add(plane);
    }

    public void DrawLimitsRegression1D(float xMin, float xMax)
    {
        for (int i = 0; i <= 100; i++)
        {
            float x = Mathf.Lerp(xMin, xMax, i / 100f);
            double[,] test = { { x } };
            double[] pred = main.PredictRegressionModel(test);
            float y = (float)pred[0];

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(x, y, 0f);
            cube.transform.localScale = Vector3.one * pointSize;
            cube.GetComponent<Renderer>().material.color = Color.green;
            points.Add(cube);
        }
    }

    // PMC Classification
    public void DrawLimitsPMCClassification(float xMin, float xMax, float yMin, float yMax, int outputSize = 1)
    {
        for (int i = 0; i <= 30; i++)
        {
            for (int j = 0; j <= 30; j++)
            {
                float x = Mathf.Lerp(xMin, xMax, i / 30f);
                float y = Mathf.Lerp(yMin, yMax, j / 30f);


                double[] test = { x, y };


                double[] pred = main.PredictPMCModel(test, true, outputSize);



                Color color = Color.black;

                // binaire classification
                if (outputSize == 1)
                {
                    if (pred[0] > 0) color = new Color(0.5f, 0.5f, 1f, 0.3f);
                    else color = new Color(1f, 0.5f, 0.5f, 0.3f);
                }
                // multi-classes (ex: 3 classes)
                else
                {

                    int index = 0;
                    double maxVal = pred[0];
                    for (int k = 1; k < outputSize; k++)
                    {
                        if (pred[k] > maxVal) // nouvelle valeur max
                        {
                            index = k; // nouvelle classe
                            maxVal = pred[k]; // mise  jour de la valeur max
                        }
                    }
                    if (index == 0) color = new Color(0.5f, 0.5f, 1f, 0.3f);
                    else if (index == 1) color = new Color(1f, 0.5f, 0.5f, 0.3f);
                    else if (index == 2) color = new Color(0.5f, 1f, 0.5f, 0.3f);
                }
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x, y, 0.1f);
                cube.transform.localScale = Vector3.one * pointSize;
                cube.GetComponent<Renderer>().material.color = color;
                points.Add(cube);
            }
        }
    }
    // PMC Regression
    public void DrawLimitsPMCRegression(float xMin, float xMax, float yMin, float yMax)
    {
        int resolution = 20;
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        int[] triangles = new int[resolution * resolution * 6];
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i <= resolution; i++)
        {
            for (int j = 0; j <= resolution; j++)
            {
                float x = Mathf.Lerp(xMin, xMax, i / (float)resolution);
                float y = Mathf.Lerp(yMin, yMax, j / (float)resolution);
                
                double[] test = { x, y };
                double[] pred = main.PredictPMCModel(test, false);
                float z = (float)pred[0];

                int index = i * (resolution + 1) + j;
                vertices[index] = new Vector3(x, z, y); 

                
                float t = Mathf.InverseLerp(0f, 5f, z);
                colors[index] = Color.Lerp(new Color(0f, 0f, 0f, 0.5f), new Color(0f, 1f, 0f, 0.5f), t); 
            }
        }

        int triIndex = 0;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                int topLeft = i * (resolution + 1) + j;
                int topRight = topLeft + 1;
                int bottomLeft = (i + 1) * (resolution + 1) + j;
                int bottomRight = bottomLeft + 1;

                // First triangle
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = topRight;

                // Second triangle
                triangles[triIndex++] = topRight;
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = bottomRight;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();

        GameObject plane = new GameObject("RegressionPlane");
        MeshFilter mf = plane.AddComponent<MeshFilter>();
        MeshRenderer mr = plane.AddComponent<MeshRenderer>();
        mf.mesh = mesh;

        Material mat = new Material(Shader.Find("Mobile/Particles/Alpha Blended"));
        mr.material = mat;

        points.Add(plane);
    }

    public void DrawLimitsPMCRegression1D(float xMin, float xMax)
    {
        for (int i = 0; i <= 100; i++)
        {
            float x = Mathf.Lerp(xMin, xMax, i / 100f);
            double[] test = { x };
            double[] pred = main.PredictPMCModel(test, false);
            float y = (float)pred[0];

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(x, y, 0.1f);
            cube.transform.localScale = Vector3.one * pointSize;
            cube.GetComponent<Renderer>().material.color = Color.green;
            points.Add(cube);
        }
    }
    public void DestroyPoints()
    {
        foreach (GameObject p in points)
            if (p != null)
            {
                Renderer renderer = p.GetComponent<Renderer>();
                if (renderer != null && renderer.material != null)
                {
                    Destroy(renderer.material);
                }
                Destroy(p);
            }
        points.Clear();
    }
}
