using UnityEngine;
using UnityEngine.EventSystems;

public class FinishedMealDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Shelves shelves;
    public int num;

    public Camera thisCamera;

    private Vector2 beforePosition;

    private GameObject connectedMeal;
    private float objectDepth;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beforePosition = eventData.position;

        connectedMeal = shelves.GetObjectAtIndex(num);
        if(connectedMeal == null)
        {
            return;
        }
        objectDepth = thisCamera.WorldToScreenPoint(connectedMeal.transform.position).z;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(connectedMeal)
        {
            Vector3 currentScreenPosition = eventData.position;
            currentScreenPosition.z = objectDepth;

            Vector3 previousScreenPosition = eventData.position -eventData.delta;
            previousScreenPosition.z = objectDepth;

            Vector3 currentWorldPosition = thisCamera.ScreenToWorldPoint(currentScreenPosition);

            Vector3 previousWorldPosition = thisCamera.ScreenToWorldPoint(previousScreenPosition);

            Vector3 worldDelta = currentWorldPosition - previousWorldPosition;

            connectedMeal.transform.position += worldDelta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (connectedMeal)
        {
            GameObject objectUnder = eventData.pointerCurrentRaycast.gameObject;
            if (!objectUnder)
            {
                shelves.UpdatePositions();
                return;
            }
            if (objectUnder.TryGetComponent(out CustomerDropOff dropOff))
            {
                if (dropOff.DropOff(shelves.GetRecipeAtIndex(num)))
                {
                    shelves.RemoveFinishedMeal(num);
                }
                else
                {
                    shelves.UpdatePositions();
                }
            }
            else
            {
                shelves.UpdatePositions();
            }
        }
        connectedMeal = null;
    }
}
