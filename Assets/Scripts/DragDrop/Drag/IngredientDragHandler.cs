using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //References
    [Header("References")]
    private Ingredients ingredient;
    private DraggedIngredients draggedIngredient;
    [SerializeField] AudioClip pickUpSound, dropSound, dropPotSound;
    AudioSource source;

    void Awake()
    {
        ingredient = GetComponent<Ingredients>();
        draggedIngredient = GameObject.FindFirstObjectByType<DraggedIngredients>();
        source = gameObject.AddComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedIngredient.Activate(transform.position, ingredient);
        source.PlayOneShot(pickUpSound);
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggedIngredient.MoveIngredient(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedIngredient.Deactivate();

        Ray position = CameraManager.GetCurrentCamera().ScreenPointToRay(eventData.position);

        if (Physics.Raycast(position, out RaycastHit hit))
        {
            GameObject objectUnder = hit.collider.gameObject;

            if (!objectUnder)
            {
                source.PlayOneShot(dropSound);
                return;
            }
            if (objectUnder.TryGetComponent(out IDroppedOn droppedOn))
            {
                droppedOn.DroppedOn(this.ingredient);
                source.PlayOneShot(dropPotSound);
            }

        }
    }
}
