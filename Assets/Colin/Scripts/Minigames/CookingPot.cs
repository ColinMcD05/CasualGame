using UnityEngine;
using System.Collections.Generic;

public class CookingPot : MonoBehaviour, IDroppedOn
{
    List<Ingredients> ingredientsInPot = new List<Ingredients>();
    [SerializeField] GameObject badMeal;
    Menu currentMenu;

    void Awake()
    {
        currentMenu = GameObject.Find("GameManager").GetComponent<Menu>();
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
                Instantiate(badMeal);
            }
            else
            {
                Instantiate(recipe.mealPrefab);
            }
        }
        EmptyPot();
    }

    public void EmptyPot()
    {
        ingredientsInPot.Clear();
    }
}
