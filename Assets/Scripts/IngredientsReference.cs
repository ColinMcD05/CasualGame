using UnityEngine;

public class IngredientsReference : MonoBehaviour
{
    [SerializeField] private Ingredients ingredientReference;

    public Ingredients GetIngredientReference()
    {
        return ingredientReference;
    }
}
