using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    //Inputs
    private DefaultInputActions mobileActions;
    private InputAction touch;

    void Awake()
    {
        mobileActions = new DefaultInputActions();
    }

    void OnEnable()
    {
        touch = mobileActions.UI.Click;
        touch.Enable();

        touch.performed += ProcessInitialTouch;
    }

    void OnDisable()
    {
        touch.Disable();
        touch.performed -= ProcessInitialTouch;
    }

    void ProcessInitialTouch(InputAction.CallbackContext context)
    {
        Vector2 touchLocation = Touchscreen.current.primaryTouch.position.ReadValue();

        Ingredients ingredient = FindIngredientAtLocation(touchLocation);

        Debug.Log(ingredient);
    }

    Ingredients FindIngredientAtLocation(Vector2 location)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(location);
        if(hitCollider != null)
        {
            print("Hi");
            return hitCollider.GetComponent<Ingredients>();
        }

        return null;
    }
}
