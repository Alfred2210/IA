using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MainDll : MonoBehaviour
{
    [DllImport("Training_IA")]
    private static extern void trainClassification(double[] X_data, double[] Y_data, int N, int D, double learning_rate, int num_iteration, double[] out_W_and_b);

    [DllImport("Training_IA")]
    private static extern void predictClassification(double[] X_test_data, int N_test, int D, double bias, double[] W_data, double[] output_prediction); // N nombre de ligne et D de colonne du data test

    [DllImport("Training_IA")]
    private static extern void trainRegression(double[] X_data, double[] Y_data, int N, int D, double[] out_W_and_b);

    [DllImport("Training_IA")]
    private static extern void predictRegression(double[] X_test_data, int N_test, int D, double bias, double[] W_data, double[] output_prediction);

    [DllImport("Training_IA")]
    private static extern IntPtr createPMC(int [] npl_data, int npl_size);

    [DllImport("Training_IA")]
    private static extern void trainPMC(IntPtr modelPtr, double[] X_flat, double[] Y_flat, int nb_samples, int input_size, int output_size, int iteration, double learning_rate, bool is_classification);

    [DllImport("Training_IA")]
    private static extern void predictPMC(IntPtr modelPtr, double[] input_data, int input_size, bool is_classification, double[] output_data);

    [DllImport("Training_IA")]
    private static extern void deletePMC(IntPtr model_ptr);

    private double[] classificationWeights;
    private double classificationBias;
    private double[] regressionWeights;
    private double regressionBias;
    private IntPtr pmcModelPtr = IntPtr.Zero;
    // Classification
    public void TrainClassificationModel(double[,] X, double[] Y)
    {
        int N = X.GetLength(0);
        int D = X.GetLength(1);

        double[] X_flat = new double[N * D];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < D; j++)
            {
                X_flat[i * D + j] = X[i, j];
            }

        }


        double[] outParams = new double[D + 1];
        trainClassification(X_flat, Y, N, D, 0.01, 10000, outParams);

        // met le biais dans la première case
        classificationBias = outParams[0];
        classificationWeights = new double[D];
        Array.Copy(outParams, 1, classificationWeights, 0, D);
    }

    public double[] PredictClassificationModel(double[,] X)
    {
        int N = X.GetLength(0);
        int D = X.GetLength(1);

        double[] X_flat = new double[N * D];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < D; j++)
            {
                X_flat[i * D + j] = X[i, j];
            }

        }

        double[] predictions = new double[N];

        predictClassification(X_flat, N, D, classificationBias, classificationWeights, predictions);

        return predictions;
    }

    // Regression
    public void TrainRegressionModel(double[,] X, double[] Y)
    {
        int N = X.GetLength(0);
        int D = X.GetLength(1);
        double[] X_flat = new double[N * D];


        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < D; j++)
            {
                X_flat[i * D + j] = X[i, j];
            }

        }

        double[] outParams = new double[D + 1];
        trainRegression(X_flat, Y, N, D, outParams);

        // met le biais dans la première case
        regressionBias = outParams[0];
        regressionWeights = new double[D];
        Array.Copy(outParams, 1, regressionWeights, 0, D);
    }

    public double[] PredictRegressionModel(double[,] X)
    {
        int N = X.GetLength(0);
        int D = X.GetLength(1);

        double[] X_flat = new double[N * D];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < D; j++)
            {
                X_flat[i * D + j] = X[i, j];
            }
        }
        double[] predictions = new double[N];
        predictRegression(X_flat, N, D, regressionBias, regressionWeights, predictions);
        return predictions;
    }

    //Perceptron Multi-Couche

    public void CreatePMC(int[] structure)
    {
        // delete l'ancien modèle s'il existe
        if (pmcModelPtr != IntPtr.Zero)
        {
            deletePMC(pmcModelPtr);
        }

        // créer le nouveau modèle structure comme par ex: new int[] { 2, 3, 1 } -> entree 2 neurones, couche cachée 3 neurones, sortie 1 neurone
        pmcModelPtr = createPMC(structure, structure.Length);
    }
    public void TrainPMCModel(double[,] X, double[,] Y, int iteration, double alpha, bool isClassification)
    {
        if (pmcModelPtr == IntPtr.Zero)
        {
            Debug.LogError("Erreur : Le modèle PMC n'est pas créé !");
            return;
        }

        int nbSamples = X.GetLength(0);
        int inputSize = X.GetLength(1);
        int outputSize = Y.GetLength(1); // Y est aussi une matrice (N, Sorties)

        // comme pour classification/régression on aplatit les matrices en tableaux 1D 
        double[] X_flat = new double[nbSamples * inputSize];
        double[] Y_flat = new double[nbSamples * outputSize];

        // copie X
        for (int i = 0; i < nbSamples; i++)
            for (int j = 0; j < inputSize; j++)
                X_flat[i * inputSize + j] = X[i, j];

        // copie Y
        for (int i = 0; i < nbSamples; i++)
            for (int j = 0; j < outputSize; j++)
                Y_flat[i * outputSize + j] = Y[i, j];

        // appel DLL
        trainPMC(pmcModelPtr, X_flat, Y_flat, nbSamples, inputSize, outputSize, iteration, alpha, isClassification);
        
    }


    public double[] PredictPMCModel(double[] input, bool isClassification, int outputSize = 1)
    {
        if (pmcModelPtr == IntPtr.Zero) return null;

        double[] result = new double[outputSize];

        predictPMC(pmcModelPtr, input, input.Length, isClassification, result);
        return result;
    }
    public void DeletePMCModel()
    {
        if (pmcModelPtr != IntPtr.Zero)
        {
            deletePMC(pmcModelPtr);
            pmcModelPtr = IntPtr.Zero;
        }
    }

}