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

    public void TestNonLinearSimple2dC()
    {

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
        main.CreatePMC(new int[] { 1, 5, 1 });
        main.TrainPMCModel(X, Y, 1000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, true);

        visualizer.DrawLimitsPMCClassification(0f, 4, 0f, 4f);
        main.DeletePMCModel();
    }

    public void TestNonLinearSimple2dR()
    {

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
        main.CreatePMC(new int[] { 1, 5, 1 });
        main.TrainPMCModel(X, Y, 1000, 0.01, false);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);

        visualizer.DrawLimitsPMCRegression(0f, 4, 0f, 4f);
        main.DeletePMCModel();
    }



    public void TestXORC()
    {
        visualizer.DestroyPoints();

        //double[,] X = { { 1, 0 }, { 0, 1 }, { 0, 0 }, { 1, 1 } };
        double[,] X = {
        { Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f) },


        { Random.Range(-0.1f, 0.1f), Random.Range(0.9f, 1.1f) },


        { Random.Range(0.9f, 1.1f), Random.Range(-0.1f, 0.1f) },


        { Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f) }
    };
        double[,] Y = { { 1 }, { 1 }, { -1 }, { -1 } };

        for (int i = 0; i < 4; i++)
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i,0] > 0 ? blue : red);
        main.CreatePMC(new int[] { 1,3,1 });
        main.TrainPMCModel(X, Y, 10000, 0.1, true);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, true);

        visualizer.DrawLimitsPMCClassification(-0.5f, 1.5f, -0.5f, 1.5f);
    }

    public void TestXORR()
    {
        visualizer.DestroyPoints();

        //double[,] X = { { 1, 0 }, { 0, 1 }, { 0, 0 }, { 1, 1 } };
        double[,] X = {
        { Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f) },


        { Random.Range(-0.1f, 0.1f), Random.Range(0.9f, 1.1f) },


        { Random.Range(0.9f, 1.1f), Random.Range(-0.1f, 0.1f) },


        { Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f) }
    };
        double[,] Y = { { 1 }, { 1 }, { -1 }, { -1 } };

        for (int i = 0; i < 4; i++)
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i, 0] > 0 ? blue : red);
        main.CreatePMC(new int[] { 1, 3, 1 });
        main.TrainPMCModel(X, Y, 10000, 0.1, true);
        double[] input = { 0.5 };
        double[] prediction = main.PredictPMCModel(input, false);

        visualizer.DrawLimitsPMCRegression(-0.5f, 1.5f, -0.5f, 1.5f);
        main.DeletePMCModel();
    }
}
