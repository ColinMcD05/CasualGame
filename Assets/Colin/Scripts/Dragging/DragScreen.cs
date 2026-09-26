using UnityEngine;
using UnityEngine.EventSystems;


public class DragScreen : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] float speed = 0.01f;
    [SerializeField] bool horizontalMovement;
    [SerializeField] bool verticalMovement;
    [SerializeField] CameraBounds bounds;

    private Camera mainCamera;

    private bool canDrag = true;

    private Vector2 lastPosition;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            lastPosition = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(canDrag)
        {
            Vector2 difference = eventData.position - lastPosition;
            Vector3 v3Difference = Vector3.zero;
            if(horizontalMovement)
            {
                v3Difference.x += difference.x;
            }
            if(verticalMovement)
            {
                v3Difference.y += difference.y;
            }
            Vector3 newPosition = mainCamera.transform.position + v3Difference * speed;
            bounds.ClampBound(ref newPosition);
            newPosition.z = mainCamera.transform.position.z;
            mainCamera.transform.position = newPosition;

            lastPosition = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag)
        {

        }
    }

    public void SetCanDrag(bool drag)
    {
        canDrag = drag;
    }
}
