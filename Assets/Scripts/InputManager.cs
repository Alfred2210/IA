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

        //PMC tests
        inputActions.Player.TestNonLinearSimple2dC.performed += OnTestNonLinearSimple2dC;
        inputActions.Player.TestNonLinearSimple2dR.performed += OnTestNonLinearSimple2dR;

        inputActions.Player.TestXORC.performed += OnTestXORC;
        inputActions.Player.TestXORR.performed += OnTestXORR;
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
    public void OnTestNonLinearSimple2dC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestNonLinearSimple2dC();
        }
    }

    public void OnTestNonLinearSimple2dR(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestNonLinearSimple2dR();
        }
    }

    public void OnTestXORC(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestXORC();
        }
    }

    public void OnTestXORR(InputAction.CallbackContext context)
    {
        if (context.performed && app != null)
        {
            app.LaunchTestXORR();
        }
    }
}