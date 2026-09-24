using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggedIngredients : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool returnToStartPosition = false;

    //References
    Image image;
    Ingredients ingredient;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector3 startPosition;

    void Awake()
    {
        gameObject.SetActive(false);

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.blocksRaycasts = false;

        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            rectTransform = gameObject.AddComponent<RectTransform>();
        }

        image = GetComponent<Image>();
        if(image == null)
        {
            image = gameObject.AddComponent<Image>();
        }    
    }

    public void Activate(Vector3 spawnPoint, Ingredients ingredient)
    {
        this.ingredient = ingredient;

        image.sprite = ingredient.GetSprite();

        startPosition = spawnPoint;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        ingredient = null;
        gameObject.SetActive(false);
    }

    public void MoveIngredient(PointerEventData eventData)
    {
        RectTransform canvasRect = canvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out Vector2 localPosition))
        {
            rectTransform.anchoredPosition = localPosition;
        }
    }
}
