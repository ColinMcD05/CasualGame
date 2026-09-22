using UnityEngine;
using UnityEngine.UI;

public class Ingredients : MonoBehaviour
{
    [Header("Ingrediant Info")]
    //Ingrediant Info
    [SerializeField] string ingredientName;
    [SerializeField] Sprite sprite;

    //References
    [Header("References")]
    [SerializeField]Image image;

    Ingredients()
    {
        if(sprite && image)
        {
            image.sprite = sprite;
        }
    }

    #region Getters
    //Getters
    public string GetIngredientName()
    {
        return ingredientName;
    }


    public Sprite GetSprite()
    {
        return sprite;
    }
    #endregion

    #region Setters
    public void SetImageColor(Color newColor)
    {
        if(image)
        {
            image.color = newColor;
        }
    }
    #endregion
}
