using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    //References
    [SerializeField] private DragScreen dragScreen;

    //Inputs
    private MobileActions mobileActions;
    private InputAction touch;
    private InputAction drag;

    void Awake()
    {
        mobileActions = new MobileActions();

        touch = mobileActions.MobileAction.Touch;
        drag = mobileActions.MobileAction.Tracking;
    }

    void OnEnable()
    {
        touch.Enable();
        touch.performed += Touch;

        drag.Enable();
        drag.performed += Drag;
    }

    void OnDisable()
    {
        touch.performed -= Touch;
        touch.Disable();

        drag.performed -= Drag;
        drag.Disable();
    }

    void Touch(InputAction.CallbackContext context)
    {

    }

    private void Drag(InputAction.CallbackContext context)
    {
        
    }
}
