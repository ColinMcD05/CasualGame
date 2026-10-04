using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CookingPot : MonoBehaviour, IDroppedOn
{
    List<Ingredients> ingredientsInPot = new List<Ingredients>();
    [SerializeField] GameObject badMeal;
    Menu currentMenu;
    Shelves shelf;

    public TextMeshProUGUI text;

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
            CancelInvoke();
            Recipe recipe = currentMenu.CompareMenu(ingredientsInPot);
            if (recipe == null)
            {
                shelf.AddFinishedMeal(null, badMeal);
                ShowText("Bad Meal");
                Debug.Log("Bad Meal");
            }
            else
            {
                shelf.AddFinishedMeal(recipe);
                ShowText(recipe.recipeName);
                Debug.Log(recipe.recipeName);
            }
            Invoke("TextDisappear", 2);
        }
        EmptyPot();
    }

    public void EmptyPot()
    {
        ingredientsInPot.Clear();
    }

    void ShowText(string newText)
    {
        text.text = "You Made " + newText;
    }

    void TextDisappear()
    {
        text.text = "";
    }
}