using UnityEngine;
using UnityEngine.InputSystem;

public interface IDraggable3D
{
    abstract public void StartDrag(Vector2 screenPosition);

    abstract public void Drag(Vector2 screenPosition);
}
