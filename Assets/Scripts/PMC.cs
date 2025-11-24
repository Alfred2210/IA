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
        Debug.Log("PMC = Touche : X : LinearSimple (Classification) | C : LinearMultiple (Classification) | V : XOR (Classification) | B : Cross (Classification) | N : MultiLinear3Classes (Classification) | F : MultiCross (Classification) | G : LinearSimple2d (Regression) | H : NonLinearSimple2d (Regression) | J : LinearSimple3d (Regression) | K : LinearTrick3d (Regression) | L : NonLinearSimple3d (Regression) ");
        visualizer.DestroyPoints();

        double[,] X =
            {
             { Random.Range(0f, 1f) },
             { Random.Range(0f, 2f) },
             { Random.Range(0f, 3f) }
          };

        double[,] Y = {
            { 2 },
            { 3 },
            { 2.5 }
        };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i, 0], blue);
        }
        main.CreatePMC(new int[] { 2,1 });
        main.TrainPMCModel(X, Y, 100000, 0.01, false);
        double[] input = { 0.5 , 0.5 };
        double[] prediction = main.PredictPMCModel(input, true);

        visualizer.DrawLimitsPMCClassification(0f, 4, 0f, 4f);
        main.DeletePMCModel();

    }

    public void TestLinearMultiple()
    {
        visualizer.DestroyPoints();
        double[,] X = {
        { 1.0, 0.0 },
        { 0.0, 1.0 },
        { 1.0, 1.0 },
        { 0.0, 0.0 }
    };
        double[,] Y = { { 1 }, { 1 }, { -1 }, { -1 } };
        for (int i = 0; i < 4; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i, 0] > 0 ? blue : red);
        }
        main.CreatePMC(new int[] { 2, 1 }); // neurones 
        main.TrainPMCModel(X, Y, 10000, 0.1, true);
        double[] input = { 0.5,0.5 };
        double[] prediction = main.PredictPMCModel(input, true);
        visualizer.DrawLimitsPMCClassification(-0.5f, 1.5f, -0.5f, 1.5f);
        main.DeletePMCModel();
    }

    public void TestXOR()
    {
        visualizer.DestroyPoints();

        //double[,] X = { { 1, 0 }, { 0, 1 }, { 0, 0 }, { 1, 1 } };
        double[,] X = {
        { 1, 0 },
        { 0, 1 },
        {0, 0 },
        { 1, 1 }
    };
        double[,] Y = { { 1 }, { 1 }, { -1 }, { -1 } };

        for (int i = 0; i < 4; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i, 0] > 0 ? blue : red);
        }

        main.CreatePMC(new int[] { 2, 2, 1 }); // neurones 
        main.TrainPMCModel(X, Y, 10000, 0.1, true);

        double[] input = { 0.5 ,0.5};
        double[] prediction = main.PredictPMCModel(input, true);

        visualizer.DrawLimitsPMCClassification(-0.5f, 1.5f, -0.5f, 1.5f);
        main.DeletePMCModel();
    }

    
    public void TestCross()
    {
        visualizer.DestroyPoints();
        double[,] X = {
        { 1, 0 },
        { 0, 1 },
        {0, 0 },
        { 1, 1 }
    };
        double[,] Y = { { 1 }, { 1 }, { -1 }, { -1 } };
        for (int i = 0; i < 4; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i, 0] > 0 ? blue : red);
        }
        main.CreatePMC(new int[] { 2, 4, 1 }); // neurones 
        main.TrainPMCModel(X, Y, 10000, 0.1, true);
        double[] input = { 0.5,0.5 };
        double[] prediction = main.PredictPMCModel(input, true);
        visualizer.DrawLimitsPMCClassification(-0.5f, 1.5f, -0.5f, 1.5f);
        main.DeletePMCModel();
    }

    public void TestMultiLinear3Classes()
    {
        Debug.Log("PMC = Touche : X : LinearSimple (Classification) | C : LinearMultiple (Classification) | V : XOR (Classification) | B : Cross (Classification) | N : MultiLinear3Classes (Classification) | F : MultiCross (Classification) | G : LinearSimple2d (Regression) | H : NonLinearSimple2d (Regression) | J : LinearSimple3d (Regression) | K : LinearTrick3d (Regression) | L : NonLinearSimple3d (Regression) ");

        visualizer.DestroyPoints();
        int N = 1000;
        double[,] X = new double[N, 2];
        double[,] Y = new double[N, 3];

        for (int i = 0; i < N; i++)
        {

            float x = Random.Range(0f, 1f);
            float y = Random.Range(0f, 1f);

            X[i, 0] = x;
            X[i, 1] = y;
            Color c = Color.black;

            if (Mathf.Abs(x) < 0.3f || Mathf.Abs(y) < 0.3f)
            {
                Y[i, 0] = 1; Y[i, 1] = -1; Y[i, 2] = -1; // [1, -1, -1]
                c = Color.blue;
            }
            else if (x * y < 0)
            {
                Y[i, 0] = -1; Y[i, 1] = 1; Y[i, 2] = -1; // [-1, 1, -1]
                c = Color.red;
            }
            else
            {
                Y[i, 0] = -1; Y[i, 1] = -1; Y[i, 2] = 1; // [-1, -1, 1]
                c = Color.green;
            }

            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], c);
        }

        main.CreatePMC(new int[] { 2,3}); // neurones 
        main.TrainPMCModel(X, Y, 1000, 0.1, true);
        double[] input = { 0.5 ,0.5};
        double[] prediction = main.PredictPMCModel(input, true,3);
        visualizer.DrawLimitsPMCClassification(-0.5f, 1.5f, -0.5f, 1.5f,3);
        main.DeletePMCModel();
    }

    public void TestMultiCross()
    {
        Debug.Log("PMC = Touche : X : LinearSimple (Classification) | C : LinearMultiple (Classification) | V : XOR (Classification) | B : Cross (Classification) | N : MultiLinear3Classes (Classification) | F : MultiCross (Classification) | G : LinearSimple2d (Regression) | H : NonLinearSimple2d (Regression) | J : LinearSimple3d (Regression) | K : LinearTrick3d (Regression) | L : NonLinearSimple3d (Regression) ");

        visualizer.DestroyPoints();
        int nbPoints = 100;

        double[,] X = new double[nbPoints, 2];
        double[,] Y = new double[nbPoints, 3]; // 3 classes = 3 colonnes

        for (int i = 0; i < nbPoints; i++)
        {
            // aléatoire entre -1 et 1
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);

            // On remplit X
            X[i, 0] = x;
            X[i, 1] = y;

            Color c = Color.white;

            // Si x est proche de 0 OU y est proche de 0
            if (Mathf.Abs(x) < 0.3f || Mathf.Abs(y) < 0.3f)
            {
                Y[i, 0] = 1; Y[i, 1] = -1; Y[i, 2] = -1; // [1, -1, -1]
                c = Color.blue;
            }
            else if (x * y < 0)
            {
                Y[i, 0] = -1; Y[i, 1] = 1; Y[i, 2] = -1; // [-1, 1, -1]
                c = Color.red;
            }
            else
            {
                Y[i, 0] = -1; Y[i, 1] = -1; Y[i, 2] = 1; // [-1, -1, 1]
                c = Color.green;
            }
            visualizer.CreatePoint(x, y, c);
        }
        main.CreatePMC(new int[] { 2, 5, 5, 1 });
        main.TrainPMCModel(X, Y, 50000, 0.01, true);
        double[] input = { 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, true,3);
        visualizer.DrawLimitsPMCClassification(-1f, 1f, -1f, 1f, 3);
        main.DeletePMCModel();
    }


    //Régression tests PMC

    public void TestLinearSimple2D()
    {
        visualizer.DestroyPoints();
        double[,] X =
     {
        { 1 },
        { 2 },
    };
        double[,] Y = {
            { 2 },
            { 3 }
        };
        for (int i = 0; i < 10; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i, 0], blue);
        }
        main.CreatePMC(new int[] { 1, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);
        visualizer.DrawLimitsPMCRegression(0f, 4, 0f, 4f);
        main.DeletePMCModel();
    }

    public void TestNonLinearSimple2D()
    {

        visualizer.DestroyPoints();

        double[,] X =
            {
             { 1 },
             { 2 },
             { 3 }
          };

        double[,] Y = {
            { 2 },
            { 3 },
            { 2.5 }
        };

        for (int i = 0; i < 10; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i, 0], Color.blue);
        }
        main.CreatePMC(new int[] { 1, 10, 1 });
        main.TrainPMCModel(X, Y, 1000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);

        visualizer.DrawLimitsPMCRegression(0f, 4, 0f, 4f);
        main.DeletePMCModel();
    }

    public void TestLinearSimple3D()
    {
        visualizer.DestroyPoints();

        double[,] X =
            {
             { 1,1 },
             { 2,2 },
             { 3,1 }
          };

        double[,] Y = {
            { 2 },
            { 3 },
            { 2.5 }
        };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i, 0], blue);
        }
        main.CreatePMC(new int[] { 1, 5, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);

        visualizer.DrawLimitsPMCRegression(0f, 4, 0f, 4f);
        main.DeletePMCModel();
    }

    public void TestLinearTrick3d()
    {

        visualizer.DestroyPoints();
        double[,] X =
            {
             { 1,1 },
             {2,2 },
             { 3,3 }
          };
        double[,] Y = { { 1 }, { 2 }, { 3 } };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i,0], blue);

        }
        main.CreatePMC(new int[] { 1, 5, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);
        visualizer.DrawLimitsPMCRegression(0f, 4, 0f, 4f);

        main.DeletePMCModel();
    }
    public void TestNonLinearSimple3d()
    {

        visualizer.DestroyPoints();

        double[,] X = {
        { 1.0, 0.0 },
        { 0.0, 1.0 },
        { 1.0, 1.0 },
        { 0.0, 0.0 }
    };

        double[,] Y = { { 2 }, { 1 }, { -2 }, { -1 } };

        for (int i = 0; i < 4; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i,0], blue);
        }
        main.CreatePMC(new int[] { 3, 5, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.05, false);
        double[] input = { 0.5, 0.5, 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);
        visualizer.DrawLimitsPMCRegression(-0.5f, 1.5f, -2.5f, 2.5f);
        main.DeletePMCModel();

    }

}
