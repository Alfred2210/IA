using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public AppController app;
    private InputController inputActions;


    void Awake()
    {
        
        inputActions = new InputController();

        //événements
        inputActions.Player.TestLinearSimple.performed += OnTestLinearSimple;
        inputActions.Player.TestLinearMultiple.performed += OnTestLinearMultiple;
        inputActions.Player.TestXOR.performed += OnTestXOR;
        inputActions.Player.TestCross.performed += OnTestCross;


        inputActions.Player.TestLinearSimple2d.performed += OnTestLinearSimple2d;
        inputActions.Player.TestNonLinearSimple2d.performed += OnTestNonLinearSimple2d;
        inputActions.Player.TestLinearSimple3d.performed += OnTestLinearSimple3d;
        inputActions.Player.TestLinearTrick3d.performed += onTestLinearTrick3d;
        inputActions.Player.TestNonLinearSimple3d.performed += OnTestNonLinearSimple3d;

        //PMC tests Classification
        inputActions.Player.TestLinearSimplePMC.performed += OnTestLinearSimplePMC;
        inputActions.Player.TestXORPMC.performed += OnTestXORPMC;
        
        inputActions.Player.TestLinearMultiplePMC.performed += OnTestLinearMultiplePMC;
        inputActions.Player.TestCrossPMC.performed += OnTestCrossPMC;
        inputActions.Player.TestMultiLinear3Classes.performed += OnTestMultiLinear3ClassesPMC;
        inputActions.Player.TestMultiCross.performed += OnTestMultiCrossPMC;
        //PMC tests Regression
        inputActions.Player.TestLinearSimple2DPMC.performed += OnTestLinearSimple2dPMC;
        inputActions.Player.TestNonLinearSimple2DPMC.performed += OnTestNonLinearSimple2dPMC;
        inputActions.Player.TestLinearSimple3DPMC.performed += OnTestLinearSimple3dPMC;
        inputActions.Player.TestLinearTrick3DPMC.performed += OnTestLinearTrick3dPMC;
        inputActions.Player.TestNonLinearSimple3DPMC.performed += OnTestNonLinearSimple3dPMC;

    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void OnDestroy()
    {
        inputActions.Dispose();
    }

    //Classification tests
    public void OnTestLinearSimple(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearSimple();
        }
    }
    public void OnTestLinearMultiple(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearMultiple();
        }
    }

    public void OnTestXOR(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestXOR();
        }
    }

    public void OnTestCross(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestCross();
        }
    }

    //Regression tests

    public void OnTestLinearSimple2d(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearSimple2d();
        }
    }

    public void OnTestNonLinearSimple2d(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestNonLinearSimple2d();
        }
    }

    public void OnTestLinearSimple3d(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearSimple3d();
        }
    }


    public void onTestLinearTrick3d(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearTrick3d();
        }
    }

    public void OnTestNonLinearSimple3d(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestNonLinearSimple3d();
        }
    }
    //PMC tests
   
    public void OnTestLinearSimplePMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearSimplePMC();
        }
    }

    public void OnTestXORPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestXORPMC();
        }
    }

    public void OnTestLinearMultiplePMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearMultiplePMC();
        }
    }

    public void OnTestCrossPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestCrossPMC();
        }
    }

    public void OnTestMultiLinear3ClassesPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestMultiLinear3ClassesPMC();
        }
    }

    public void OnTestMultiCrossPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestMultiCrossPMC();
        }
    }
    public void OnTestLinearSimple2dPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearSimple2dPMC();
        }
    }
    public void OnTestNonLinearSimple2dPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestNonLinearSimple2dPMC();
        }
    }

    public void OnTestLinearSimple3dPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearSimple3dPMC();
        }
    }

    public void OnTestLinearTrick3dPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestLinearTrick3dPMC();
        }
    }
    public void OnTestNonLinearSimple3dPMC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestNonLinearSimple3dPMC();
        }
    }



}