using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag Settings")]
    [SerializeField] private float dragAlpha;
    [SerializeField] private bool retrnToStartPosition = false;

    //References
    [Header("References")]
    private Ingredients ingredient;
    private DraggedIngredients draggedIngredient;

    //Back end variables
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private GameObject dragVisualInstance;

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
    }
}
