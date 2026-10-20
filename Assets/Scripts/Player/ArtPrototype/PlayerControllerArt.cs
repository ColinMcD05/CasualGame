using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerArt : MonoBehaviour
{
    public float speed;

    public float lookSensitivity;
    public float lookAngleLimit;

    private Vector3 movementVector;
    private float lookAngle;
    private Camera mainCamera;
    private CharacterController characterController;

    private PrototypeActions actions;
    private InputAction move;
    private InputAction look;

    void Awake()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();

        actions = new PrototypeActions();
        move = actions.Player.Move;
        look = actions.Player.Look;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        Vector2 moveVector = move.ReadValue<Vector2>();
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());

        Look(mouseDelta);
        Move(moveVector);
    }
    public void Move(Vector2 moveVector)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = movementVector.y;

        Vector2 newSpeed = new Vector2(moveVector.y * speed, moveVector.x * speed);

        movementVector = (forward * newSpeed.x) + (right * newSpeed.y);
        movementVector.y = 0;

        characterController.Move(movementVector * Time.deltaTime);
    }

    private void Look(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * lookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -lookAngleLimit, lookAngleLimit);

        mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * lookSensitivity, 0);
    }
}
