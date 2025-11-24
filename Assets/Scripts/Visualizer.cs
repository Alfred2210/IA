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
        sphere.transform.position = new Vector3(x, y, z);
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

                // afficher les pr dictions avec un d grad  de couleur
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = new Vector3(x, y, 0.1f);
                quad.transform.localScale = Vector3.one * pointSize * 1.0f;

                // couleur selon la classe pr dite
                Color color = pred[0] > 0 ? new Color(0.5f, 0.5f, 1f, 0.3f) : new Color(1f, 0.5f, 0.5f, 0.3f);
                quad.GetComponent<Renderer>().material.color = color;
                points.Add(quad);
            }
        }
    }

    // Regression
    public void DrawLimitsRegression(float xMin, float xMax, float yMin, float yMax)
    {
        int resolution = 40;

        for (int i = 0; i <= 30; i++)
        {
            for (int j = 0; j <= 30; j++)
            {
                float x = Mathf.Lerp(xMin, xMax, i / 30f);
                float y = Mathf.Lerp(yMin, yMax, j / 30f);


                double[,] test = { { x, y } };
                double[] pred = main.PredictRegressionModel(test);

                float z = (float)pred[0];
                float t = Mathf.InverseLerp(-3f, 3f, z);
                // couleur selon la classe pr dite
                Color color = Color.Lerp(new Color(0.5f, 0.5f, 1f, 0.3f), new Color(1f, 0.5f, 0.5f, 0.3f), t);
                color.a = 0.6f;

                // afficher les pr dictions avec un d grad  de couleur
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = new Vector3(x, y, 0.1f);

                float stepX = (xMax - xMin) / resolution;
                float stepY = (yMax - yMin) / resolution;
                quad.transform.localScale = new Vector3(stepX * 1.05f, stepY * 1.05f, 1f);
                quad.GetComponent<Renderer>().material.color = color;
                points.Add(quad);
            }
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
        for (int i = 0; i <= 30; i++)
        {
            for (int j = 0; j <= 30; j++)
            {
                float x = Mathf.Lerp(xMin, xMax, i / 30f);
                float y = Mathf.Lerp(yMin, yMax, j / 30f);

                double[] test = { x, y };


                double[] pred = main.PredictPMCModel(test, false);


                float z = (float)pred[0];


                float t = Mathf.InverseLerp(-3f, 3f, z);
                Color color = Color.Lerp(Color.black, Color.greenYellow, t);
                color.a = 1f;

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x, y, 0.1f);
                cube.transform.localScale = Vector3.one * pointSize;

                cube.GetComponent<Renderer>().material.color = color;
                points.Add(cube);
            }
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