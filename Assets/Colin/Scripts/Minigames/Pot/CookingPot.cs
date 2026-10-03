using UnityEngine;
using System.Collections.Generic;

public class CookingPot : MonoBehaviour, IDroppedOn
{
    List<Ingredients> ingredientsInPot = new List<Ingredients>();
    [SerializeField] GameObject badMeal;
    Menu currentMenu;
    Shelves shelf;

    void Awake()
    {
        currentMenu = GameObject.FindFirstObjectByType<Menu>();
        shelf = GameObject.FindFirstObjectByType<Shelves>();
    }

    public void DroppedOn(Ingredients ingredient)
    {
        ingredientsInPot.Add(ingredient);
        Debug.Log(ingredient.GetIngredientName());
    }

    public void MakeMeal()
    {
        if(currentMenu)
        {
            Recipe recipe = currentMenu.CompareMenu(ingredientsInPot);
            if (recipe == null)
            {
                shelf.AddFinishedMeal(null, badMeal);
                Debug.Log("Bad Meal");
            }
            else
            {
                shelf.AddFinishedMeal(recipe);
                Debug.Log(recipe.recipeName);
            }
        }
        EmptyPot();
    }

    public void EmptyPot()
    {
        ingredientsInPot.Clear();
    }
}