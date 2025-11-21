using UnityEngine;

public class AppController : MonoBehaviour
{
    public MainDll main;
    public Visualizer visualizer;

    
    private Classification classification;
    private Regression regression;
    private PMC pmc;


    void Start()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 5f;
        Camera.main.transform.position = new Vector3(0, 0, -10);

        visualizer.Initialize(main);

        classification = new Classification(main, visualizer);
        regression = new Regression(main, visualizer);
        pmc = new PMC(main, visualizer);

    }

    // Classification tests
    public void LaunchTestLinearSimple()
    {
        if (classification != null)
            classification.TestLinearSimple();
    }

    public void LaunchTestLinearMultiple()
    {
        if (classification != null)
            classification.TestLinearMultiple();
    }

    public void LaunchTestXOR()
    {
        if (classification != null)
            classification.TestXOR();
    }

    public void LaunchTestCross()
    {
        if (classification != null)
            classification.TestCross();
    }
    //Regression tests
    public void LaunchTestLinearSimple2d()
    {
        if (regression != null)
            regression.TestLinearSimple2d();
    }

    public void LaunchTestNonLinearSimple2d()
    {
        if (regression != null)
            regression.TestNonLinearSimple2d();
    }

    public void LaunchTestLinearSimple3d()
    {
        if (regression != null)
            regression.TestLinearSimple3d();
    }
    public void LaunchTestLinearTrick3d()
    {
        if (regression != null)
            regression.TestLinearTrick3d();
    }

    public void LaunchTestNonLinearSimple3d()
    {
        if (regression != null)
            regression.TestNonLinearSimple3d();
    }

    // PMC tests
    public void LaunchTestNonLinearSimple2dC()
    {
        if (pmc != null)
            pmc.TestNonLinearSimple2dC();
    }
    public void LaunchTestNonLinearSimple2dR()
    {
        if (pmc != null)
            pmc.TestNonLinearSimple2dR();
    }

    public void LaunchTestXORC()
    {
        if (pmc != null)
            pmc.TestXORC();
    }

    public void LaunchTestXORR()
    {
        if (pmc != null)
            pmc.TestXORR();
    }
}
