using UnityEngine;
using UnityEngine.InputSystem;

public abstract class IDragable
{
    abstract public void StartDrag(InputAction.CallbackContext context);

    abstract public void Drag(InputAction.CallbackContext context);
}
