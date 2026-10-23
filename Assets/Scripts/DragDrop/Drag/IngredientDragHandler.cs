using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //References
    [Header("References")]
    private Ingredients ingredient;
    private DraggedIngredients draggedIngredient;
    [SerializeField] AudioClip pickUpSound, dropSound, dropPotSound;
    Inventory inventory;

    bool canPickUp;

    AudioSource source;

    void Awake()
    {
        ingredient = GetComponent<Ingredients>();
        draggedIngredient = GameObject.FindFirstObjectByType<DraggedIngredients>();
        source = gameObject.AddComponent<AudioSource>();
        inventory = GameObject.FindFirstObjectByType<Inventory>();
    }

    void Start()
    {
        CheckCanPickup();
    }

    public void OnEnable()
    {
        Inventory.OnInventoryChange += CheckCanPickup;
    }

    public void OnDisable()
    {
        Inventory.OnInventoryChange -= CheckCanPickup;
    }

    void CheckCanPickup()
    {
        canPickUp = inventory.InInventory(ingredient);
        
        if(canPickUp)
        {
            ingredient.SetImageColor(Color.white);
        }
        else
        {
            ingredient.SetImageColor(Color.gray);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canPickUp)
            return;

        draggedIngredient.Activate(transform.position, ingredient);
        source.PlayOneShot(pickUpSound);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canPickUp)
            return;

        draggedIngredient.MoveIngredient(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canPickUp)
            return;

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
