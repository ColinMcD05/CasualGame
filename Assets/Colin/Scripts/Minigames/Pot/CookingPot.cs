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
    }

    public void DroppedOn(Ingredients ingredient)
    {
        ingredientsInPot.Add(ingredient);
    }

    public void MakeMeal()
    {
        if(currentMenu)
        {
            Recipe recipe = currentMenu.CompareMenu(ingredientsInPot);
            if (recipe == null)
            {
                shelf.AddFinishedMeal(null);
            }
            else
            {
                shelf.AddFinishedMeal(recipe);
            }
        }
        EmptyPot();
    }

    public void EmptyPot()
    {
        ingredientsInPot.Clear();
    }
}