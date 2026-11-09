using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodBaskets : MonoBehaviour
{
    [SerializeField] private GameObject ingredients;
    Image ingredientImage;
    [SerializeField] Camera cauldronCamera;

    Inventory inventory;
    [SerializeField] TextMeshPro numberText;

    void Awake()
    {
        //cauldronCamera = GameObject.FindGameObjectWithTag("CauldronCamera").GetComponent<Camera>();

        GameObject ingredient = GameObject.Find(ingredients.name);
        if (ingredient)
        {
            ingredients = ingredient;
        }
        else
        {
            ingredients = Instantiate(ingredients);
        }

        ingredientImage = ingredients.GetComponent<Image>();

        UpdatePosition();

        inventory = GameObject.FindFirstObjectByType<Inventory>();
        Inventory.OnInventoryChange += ChangeNumber;
    }

    public void Start()
    {
        UpdatePosition();
        ChangeNumber();
    }

    public void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (ingredientImage)
        {
            ingredientImage.rectTransform.position = cauldronCamera.WorldToScreenPoint(transform.position);
        }
    }

    void ChangeNumber()
    {
        int number = inventory.GetIngredientAmount(ingredients.GetComponent<Ingredients>());

        if (number <= 0)
        {
            numberText.text = "";
        }
        else
        {
            numberText.text = $"{number}";
        }
    }
}