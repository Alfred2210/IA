using System.Collections.Generic;
using UnityEngine;

public class Classification
{
  
    private MainDll main;
    private Visualizer visualizer;

    private Color blue = Color.blue;
    private Color red = Color.red;



    public Classification(MainDll main, Visualizer visua)
    {
        this.main = main;
        this.visualizer = visua;


    }

   public void TestLinearSimple()
    {
        visualizer.DestroyPoints();

        double[,] X = {
        // point 1 Bleu en bas à gauche
        { 1, 1 }, 
        
        // point 2 Rouge en haut à droite
        { 2, 3 }, 
        
        // point 3 Rouge en haut à droite
        { 3, 3 }
    };
        double[] Y = { 1, -1, -1 };

        for (int i = 0; i < 3; i++)
        {
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i] > 0 ? blue : red);
        }
        main.TrainClassificationModel(X, Y);
        visualizer.DrawLimitsClassification(0f, 4, 0f, 4f);

        Debug.Log("Classification = Touche : Haut : Linear Simple | Gauche: Linear Multiple | Bas: XOR | Droite: Cross ");
    }

    public void TestLinearMultiple()
    {
        visualizer.DestroyPoints();

        List<double[]> X_list = new List<double[]>();
        List<double> Y_list = new List<double>();

        for (int i = 0; i < 10; i++)
        {
            // Classe 1
            float x1 = Random.Range(0.5f, 1.5f);
            float y1 = Random.Range(0.5f, 1.5f);
            X_list.Add(new double[] { x1, y1 });
            Y_list.Add(1);
            visualizer.CreatePoint(x1, y1, blue);

            // Classe -1  
            float x2 = Random.Range(2.5f, 3.5f);
            float y2 = Random.Range(2.5f, 3.5f);
            X_list.Add(new double[] { x2, y2 });
            Y_list.Add(-1);
            visualizer.CreatePoint(x2, y2, red);
        }

        double[,] X = new double[X_list.Count, 2];
        double[] Y = Y_list.ToArray();
        for (int i = 0; i < X_list.Count; i++)
        {
            X[i, 0] = X_list[i][0];
            X[i, 1] = X_list[i][1];
        }

        main.TrainClassificationModel(X, Y);
        visualizer.DrawLimitsClassification(0f, 4, 0f, 4f);
    }

    public void TestXOR()
    {
        visualizer.DestroyPoints();

        //double[,] X = { { 1, 0 }, { 0, 1 }, { 0, 0 }, { 1, 1 } };
        double[,] X = {
        { Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f) },


        { Random.Range(-0.1f, 0.1f), Random.Range(0.9f, 1.1f) },


        { Random.Range(0.9f, 1.1f), Random.Range(-0.1f, 0.1f) },


        { Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f) }
    };
        double[] Y = { 1, 1, -1, -1 };

        for (int i = 0; i < 4; i++)
            visualizer.CreatePoint((float)X[i, 0], (float)X[i, 1], Y[i] > 0 ? blue : red);

        main.TrainClassificationModel(X, Y);
        visualizer.DrawLimitsClassification(-0.5f, 1.5f, -0.5f, 1.5f);
    }

    public void TestCross()
    {
        visualizer.DestroyPoints();

        List<double[]> X_list = new List<double[]>();
        List<double> Y_list = new List<double>();

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            bool inCross = Mathf.Abs(x) <= 0.3f || Mathf.Abs(y) <= 0.3f;

            X_list.Add(new double[] { x, y });
            Y_list.Add(inCross ? 1 : -1);
            visualizer.CreatePoint(x, y, inCross ? blue : red);
        }

        double[,] X = new double[X_list.Count, 2];
        double[] Y = Y_list.ToArray();
        for (int i = 0; i < X_list.Count; i++)
        {
            X[i, 0] = X_list[i][0];
            X[i, 1] = X_list[i][1];
        }

        main.TrainClassificationModel(X, Y);
        visualizer.DrawLimitsClassification(-1.5f, 1.5f, -1.5f, 1.5f);
    }


}
