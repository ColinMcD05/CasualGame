using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //References
    [Header("References")]
    private Ingredients ingredient;
    private DraggedIngredients draggedIngredient;

    void Awake()
    {
        ingredient = GetComponent<Ingredients>();
        draggedIngredient = GameObject.FindFirstObjectByType<DraggedIngredients>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedIngredient.Activate(transform.position, ingredient);
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggedIngredient.MoveIngredient(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedIngredient.Deactivate();

        GameObject objectUnder = eventData.pointerCurrentRaycast.gameObject;
        if(!objectUnder)
        {
            return;
        }
        if(objectUnder.TryGetComponent(out IDroppedOn droppedOn))
        {
            droppedOn.DroppedOn(this.ingredient);
        }
    }
}
