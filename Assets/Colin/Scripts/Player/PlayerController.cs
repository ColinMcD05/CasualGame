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

    void Awake()
    {
        mobileActions = new MobileActions();

        touch = mobileActions.MobileAction.Touch;
    }

    void OnEnable()
    {
        touch.Enable();
    }

    void OnDisable()
    {
        touch.Disable();
    }
    public InputAction GetTouch()
    {
        return touch;
    }
}
