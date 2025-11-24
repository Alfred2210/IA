using UnityEngine;

public class Regression
{
    private MainDll main;
    private Visualizer visualizer;

    private Color blue = Color.blue;
    


    public Regression(MainDll main, Visualizer visualizer)
    {
        this.main = main;
        this.visualizer = visualizer;
    }

    public void TestLinearSimple2d()
    {

        visualizer.DestroyPoints();

        double[,] X =
     {
        { 1 }, 
        { 2 },
    };
        double[] Y = { 2, 3 };

        for (int i = 0; i < 2; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i], blue);
        }
        main.TrainRegressionModel(X, Y);
        visualizer.DrawLimitsRegression(0f, 4, 0f, 4f);

        Debug.Log("Regression = Touche : E : Linear Simple 2D | R : LinearSimple3d | T : LinearTrick3d | Y : NonLinearSimple2d | U : NonLinearSimple3d ");
    }

    public void TestNonLinearSimple2d()
    {

        visualizer.DestroyPoints();

        double[,] X =
            {
             { Random.Range(0f, 1f) },
             { Random.Range(0f, 2f) },
             { Random.Range(0f, 3f) }
          };

        double[] Y = { 2, 3, 2.5 };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)Y[i], blue);
        }
        main.TrainRegressionModel(X, Y);
        visualizer.DrawLimitsRegression(0f, 4, 0f, 4f);

    }

    public void TestLinearSimple3d()
    {
        visualizer.DestroyPoints();

        double[,] X =
            {
             { Random.Range(0f, 1f), Random.Range(0f, 1f) },
             { Random.Range(0f, 2f), Random.Range(0f, 2f) },
             { Random.Range(0f, 3f), Random.Range(0f, 3f) }

          };

        double[] Y = { 2, 3, 2.5 };
        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i], blue);
        }
        main.TrainRegressionModel(X, Y);
        visualizer.DrawLimitsRegression(0f, 4, 0f, 4f);
    }   

    public void TestLinearTrick3d()
    {

        visualizer.DestroyPoints();
        double[,] X =
            {
             { Random.Range(0f, 1f), Random.Range(0f, 1f) },
             { Random.Range(0f, 2f), Random.Range(0f, 2f) },
             { Random.Range(0f, 3f), Random.Range(0f, 3f) }
          };
        double[] Y = { 1, 2, 3 };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i], blue);

        }
        main.TrainRegressionModel(X, Y);
        visualizer.DrawLimitsRegression(0f, 4, 0f, 4f);
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

        double[] Y = { 2, 1, -2, -1 };

        for (int i = 0; i < 4; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], (float)Y[i], blue);
        }
        main.TrainRegressionModel(X, Y);
        visualizer.DrawLimitsRegression(-0.5f, 1.5f, -2.5f, 2.5f);
    }


}
