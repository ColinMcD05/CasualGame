using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //References
    private Camera currentCamera;

    //Inputs
    private MobileActions mobileActions;
    private InputAction touch;
    private InputAction drag;

    IDraggable3D draggedObject;

    void Awake()
    {
        mobileActions = new MobileActions();

        touch = mobileActions.MobileAction.Touch;
        drag = mobileActions.MobileAction.Tracking;
    }

    private void Start()
    {
        currentCamera = CameraManager.GetCurrentCamera();
    }

    void OnEnable()
    {
        //Touch input actions
        touch.Enable();
        touch.performed += Touch;

        drag.Enable();
        drag.performed += Drag;

        //Camera manager events
        CameraManager.OnCameraChanged += ChangeCurrentCamera;
    }

    void OnDisable()
    {
        //Touch input actions
        touch.performed -= Touch;
        touch.Disable();

        drag.performed -= Drag;
        drag.Disable();

        //Camera manager events
        CameraManager.OnCameraChanged += ChangeCurrentCamera;
    }

    void Touch(InputAction.CallbackContext context)
    {
        Vector2 screenPostion = context.ReadValue<Vector2>();
        Ray position = currentCamera.ScreenPointToRay(screenPostion);

        if (Physics.Raycast(position, out RaycastHit hit))
        {
            IDraggable3D touchedDraggable = hit.collider.gameObject.GetComponent<IDraggable3D>();
            if (touchedDraggable == null)
            {
                draggedObject = null;
                return;
            }

            draggedObject = touchedDraggable;
            draggedObject.StartDrag(screenPostion);

            Debug.Log(hit.collider.gameObject);
        }
    }

    private void Drag(InputAction.CallbackContext context)
    {
        if (draggedObject == null)
            return;

        draggedObject.Drag(context.ReadValue<Vector2>());
    }

    private void ChangeCurrentCamera(Camera newCamera)
    {
        currentCamera = newCamera;
    }
}
