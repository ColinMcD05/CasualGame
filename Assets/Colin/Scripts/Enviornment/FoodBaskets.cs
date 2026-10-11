using UnityEngine;
using UnityEngine.UI;

public class FoodBaskets : MonoBehaviour
{
    [SerializeField] private GameObject ingredients;
    Image ingredientImage;
    [SerializeField] Camera cauldronCamera;

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
    }

    public void Start()
    {
        UpdatePosition();
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
}