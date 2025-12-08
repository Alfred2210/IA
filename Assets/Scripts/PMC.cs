using UnityEngine;
using UnityEngine.Windows;

public class PMC
{
    private MainDll main;
    private Visualizer visualizer;

    private Color blue = Color.blue;
    private Color red = Color.red;



    public PMC(MainDll main, Visualizer visualizer)
    {
        this.main = main;
        this.visualizer = visualizer;
    }


    //Classification tests PMC


    public void TestLinearSimple()
    {
        visualizer.SetCamera2D();
        visualizer.DestroyPoints();

        double[,] X = {
            { 1, 1 },
            { 2, 3 },
            { 3, 3 }
        };
        double[] Y = {
            1,
            -1,
            -1
        };

        Debug.Log("debut test");
        Debug.Log($"dataset: {X.GetLength(0)} points, {X.GetLength(1)}");

        for (int i = 0; i < X.GetLength(0); i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i] > 0 ? blue : red);
            Debug.Log($"Point {i}: ({X[i, 0]}, {X[i, 1]})classe {Y[i]}");
        }

        main.TrainClassificationModel(X, Y);

        Debug.Log($"\n resultat ");
        Debug.Log($"Bias: {main.classificationBias}");
        Debug.Log($"W1: {main.classificationWeights[0]}");
        Debug.Log($"W2: {main.classificationWeights[1]}");

        // Test prédictions
        Debug.Log($"\n prediction");
        for (int i = 0; i < X.GetLength(0); i++)
        {
            double[,] testPt = { { X[i, 0], X[i, 1] } };
            double[] pred = main.PredictClassificationModel(testPt);
            Debug.Log($"point ({X[i, 0]}, {X[i, 1]}): attendu={Y[i]}, predit={pred[0]:F2}");
        }

        visualizer.DrawLimitsClassification(0f, 5f, 0f, 5f);
    }

    public void TestLinearMultiple()
    {
        visualizer.SetCamera2D();
        visualizer.DestroyPoints();
        int samplesPerClass = 50;
        int totalSamples = samplesPerClass * 2;
        double[,] X = new double[totalSamples, 2];
        double[,] Y = new double[totalSamples, 1];

        for (int i = 0; i < samplesPerClass; i++)
        {
            
            X[i, 0] = UnityEngine.Random.Range(0f, 1f) * 0.9 + 1.0;
            X[i, 1] = UnityEngine.Random.Range(0f, 1f) * 0.9 + 1.0;
            Y[i, 0] = 1;
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], blue);

            
            X[i + samplesPerClass, 0] = UnityEngine.Random.Range(0f, 1f) * 0.9 + 2.0;
            X[i + samplesPerClass, 1] = UnityEngine.Random.Range(0f, 1f) * 0.9 + 2.0;
            Y[i + samplesPerClass, 0] = -1;
            visualizer.CreatePoint((float)X[i + samplesPerClass, 0], (float)X[i + samplesPerClass, 1], red);
        }
        main.CreatePMC(new int[] { 2, 1 }); // neurones 
        main.TrainPMCModel(X, Y, 10000, 0.1, true);
        double[] input = { 0.5,0.5 };
        double[] prediction = main.PredictPMCModel(input, true);
        visualizer.DrawLimitsPMCClassification(0f, 3f, 0f, 3f);
        main.DeletePMCModel();
    }

    public void TestXOR()
    {
        visualizer.SetCamera2D();
        visualizer.DestroyPoints();

        //double[,] X = { { 1, 0 }, { 0, 1 }, { 0, 0 }, { 1, 1 } };
        double[,] X = {
            { 1, 0 },
            { 0, 1 },
            { 0, 0 },
            { 1, 1 }
        };
        double[,] Y = { { 1 }, { 1 }, { -1 }, { -1 } };

        for (int i = 0; i < 4; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i, 0] > 0 ? blue : red);
        }

        main.CreatePMC(new int[] { 2, 2, 1 }); // neurones 
        main.TrainPMCModel(X, Y, 10000, 0.1, true);

        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, true);

        visualizer.DrawLimitsPMCClassification(-0.5f, 1.5f, -0.5f, 1.5f);
        main.DeletePMCModel();
    }

    public void TestCross()
    {
        visualizer.SetCamera2D();
        visualizer.DestroyPoints();
        int nbPoints = 500;
        double[,] X = new double[nbPoints, 2];
        double[,] Y = new double[nbPoints, 1];

        for (int i = 0; i < nbPoints; i++)
        {
            float x = Random.Range(0f, 1f) * 2.0f - 1.0f;
            float y = Random.Range(0f, 1f) * 2.0f - 1.0f;
            X[i, 0] = x;
            X[i, 1] = y;

            if (Mathf.Abs(x) <= 0.3f || Mathf.Abs(y) <= 0.3f)
            {
                Y[i, 0] = 1;
                visualizer.CreatePoint(x, y, blue);
            }
            else
            {
                Y[i, 0] = -1;
                visualizer.CreatePoint(x, y, red);
            }
        }
        main.CreatePMC(new int[] { 2, 4, 1 }); // neurones 
        main.TrainPMCModel(X, Y, 10000, 0.1, true);
        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, true);
        visualizer.DrawLimitsPMCClassification(-1f, 1f, -1f, 1f);
        main.DeletePMCModel();
    }

    public void TestMultiLinear3Classes()
    {
        Debug.Log("PMC = Touche : X : LinearSimple (Classification) | C : LinearMultiple (Classification) | V : XOR (Classification) | B : Cross (Classification) | N : MultiLinear3Classes (Classification) | F : MultiCross (Classification) | G : LinearSimple2d (Regression) | H : NonLinearSimple2d (Regression) | J : LinearSimple3d (Regression) | K : LinearTrick3d (Regression) | L : NonLinearSimple3d (Regression) ");

        visualizer.SetCamera2D();
        visualizer.DestroyPoints();
        int nbPoints = 500;
        System.Collections.Generic.List<double[]> validX = new System.Collections.Generic.List<double[]>();
        System.Collections.Generic.List<double[]> validY = new System.Collections.Generic.List<double[]>();

        for (int i = 0; i < nbPoints; i++)
        {
            float x = Random.Range(0f, 1f) * 2.0f - 1.0f;
            float y = Random.Range(0f, 1f) * 2.0f - 1.0f;

            double[] yVal = new double[3];
            Color c = Color.black;
            bool keep = true;

            if (-x - y - 0.5 > 0 && y < 0 && x - y - 0.5 < 0)
            {
                yVal[0] = 1; yVal[1] = -1; yVal[2] = -1;
                c = blue;
            }
            else if (-x - y - 0.5 < 0 && y > 0 && x - y - 0.5 < 0)
            {
                yVal[0] = -1; yVal[1] = 1; yVal[2] = -1;
                c = red;
            }
            else if (-x - y - 0.5 < 0 && y < 0 && x - y - 0.5 > 0)
            {
                yVal[0] = -1; yVal[1] = -1; yVal[2] = 1;
                c = Color.green; 
            }
            else
            {
                keep = false;
            }

            if (keep)
            {
                validX.Add(new double[] { x, y });
                validY.Add(yVal);
                visualizer.CreatePoint(x, y, c);
            }
        }

        double[,] X = new double[validX.Count, 2];
        double[,] Y = new double[validY.Count, 3];
        for(int i=0; i<validX.Count; i++)
        {
            X[i, 0] = validX[i][0];
            X[i, 1] = validX[i][1];
            Y[i, 0] = validY[i][0];
            Y[i, 1] = validY[i][1];
            Y[i, 2] = validY[i][2];
        }

        main.CreatePMC(new int[] { 2, 3 }); // neurones 
        main.TrainPMCModel(X, Y, 1000, 0.1, true);
        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, true, 3);
        visualizer.DrawLimitsPMCClassification(-1f, 1f, -1f, 1f, 3);
        main.DeletePMCModel();
    }

    public void TestMultiCross()
    {
        Debug.Log("PMC = Touche : X : LinearSimple (Classification) | C : LinearMultiple (Classification) | V : XOR (Classification) | B : Cross (Classification) | N : MultiLinear3Classes (Classification) | F : MultiCross (Classification) | G : LinearSimple2d (Regression) | H : NonLinearSimple2d (Regression) | J : LinearSimple3d (Regression) | K : LinearTrick3d (Regression) | L : NonLinearSimple3d (Regression) ");

        visualizer.SetCamera2D();
        visualizer.DestroyPoints();
        int nbPoints = 1000;
        double[,] X = new double[nbPoints, 2];
        double[,] Y = new double[nbPoints, 3];

        for (int i = 0; i < nbPoints; i++)
        {
            float x = UnityEngine.Random.Range(0f, 1f) * 2.0f - 1.0f;
            float y = UnityEngine.Random.Range(0f, 1f) * 2.0f - 1.0f;
            X[i, 0] = x;
            X[i, 1] = y;

            Color c = Color.white;

           
            float xMod = (x % 0.5f + 0.5f) % 0.5f;
            float yMod = (y % 0.5f + 0.5f) % 0.5f;

            if (Mathf.Abs(xMod) <= 0.25f && Mathf.Abs(yMod) > 0.25f)
            {
                Y[i, 0] = 1; Y[i, 1] = -1; Y[i, 2] = -1;
                c = blue;
            }
            else if (Mathf.Abs(xMod) > 0.25f && Mathf.Abs(yMod) <= 0.25f)
            {
                Y[i, 0] = -1; Y[i, 1] = 1; Y[i, 2] = -1;
                c = red;
            }
            else
            {
                Y[i, 0] = -1; Y[i, 1] = -1; Y[i, 2] = 1;
                c = Color.green;
            }
            visualizer.CreatePoint(x, y, c);
        }
        main.CreatePMC(new int[] { 2, 8, 8, 3 });
        main.TrainPMCModel(X, Y, 50000, 0.01, true);
        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, true, 3);
        visualizer.DrawLimitsPMCClassification(-1f, 1f, -1f, 1f, 3);
        main.DeletePMCModel();
    }


    //Régression tests PMC

    public void TestLinearSimple2D()
    {
        visualizer.SetCamera2D();
        visualizer.DestroyPoints();
        double[,] X = {
            { 1 },
            { 2 }
        };
        double[,] Y = {
            { 2 },
            { 3 }
        };
        for (int i = 0; i < 2; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i, 0], blue);
        }
        main.CreatePMC(new int[] { 1, 1 });
        main.TrainPMCModel(X, Y, 1000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);
        visualizer.DrawLimitsPMCRegression1D(0f, 4f);
        main.DeletePMCModel();
    }

    public void TestNonLinearSimple2D()
    {
        visualizer.SetCamera2D();
        visualizer.DestroyPoints();

        double[,] X = {
            { 1 },
            { 2 },
            { 3 }
        };
        double[,] Y = {
            { 2 },
            { 3 },
            { 2.5 }
        };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i, 0], Color.blue);
        }
        main.CreatePMC(new int[] { 1, 3, 1 });
        main.TrainPMCModel(X, Y, 1000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);

        visualizer.DrawLimitsPMCRegression1D(0f, 4f);
        main.DeletePMCModel();
    }

    public void TestLinearSimple3D()
    {
        visualizer.SetCamera3D();
        visualizer.DestroyPoints();

        double[,] X = {
            { 1, 1 },
            { 2, 2 },
            { 3, 1 }
        };
        double[,] Y = {
            { 2 },
            { 3 },
            { 2.5 }
        };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i, 0], Color.yellow);
        }
        main.CreatePMC(new int[] { 2, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.01, false);
        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);

        visualizer.DrawLimitsPMCRegression(0f, 4, 0f, 4f);
        main.DeletePMCModel();
    }

    public void TestLinearTrick3d()
    {
        visualizer.SetCamera3D();
        visualizer.DestroyPoints();
        double[,] X = {
            { 1, 1 },
            { 2, 2 },
            { 3, 3 }
        };
        double[,] Y = { { 1 }, { 2 }, { 3 } };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i, 0], Color.yellow);
        }
        main.CreatePMC(new int[] { 2, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.01, false);
        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);
        visualizer.DrawLimitsPMCRegression(0f, 4, 0f, 4f);

        main.DeletePMCModel();
    }
    public void TestNonLinearSimple3d()
    {
        visualizer.SetCamera3D();
        visualizer.DestroyPoints();

        double[,] X = {
            { 1, 0 },
            { 0, 1 },
            { 1, 1 },
            { 0, 0 }
        };
        double[,] Y = { { 2 }, { 1 }, { -2 }, { -1 } };

        for (int i = 0; i < 4; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i, 0], Color.yellow);
        }
        main.CreatePMC(new int[] { 2, 2, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.01, false);
        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);
        visualizer.DrawLimitsPMCRegression(-0.5f, 1.5f, -0.5f, 1.5f);
        main.DeletePMCModel();

    }

}
