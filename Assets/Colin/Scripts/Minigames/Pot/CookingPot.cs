using UnityEngine;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(StirThePot))]
public class CookingPot : MonoBehaviour, IDroppedOn
{
    List<Ingredients> ingredientsInPot = new List<Ingredients>();
    [SerializeField] GameObject badMeal;
    [SerializeField] StirThePot stir;
    [SerializeField] TextMeshProUGUI rotationNumber;
    Menu currentMenu;
    Shelves shelf;
    int rotations;


    public TextMeshProUGUI text;

    void Awake()
    {
        currentMenu = GameObject.FindFirstObjectByType<Menu>();
        shelf = GameObject.FindFirstObjectByType<Shelves>();
        SetRotation(0);
    }

    public void DroppedOn(Ingredients ingredient)
    {
        ingredientsInPot.Add(ingredient);
        Debug.Log(ingredient.GetIngredientName());
        SetRotation(0);
    }

    public void MakeMeal()
    {
        if(currentMenu)
        {
            CancelInvoke();
            Recipe recipe = currentMenu.CompareMenu(ingredientsInPot);
            if (recipe == null || recipe.stirAmount != rotations)
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

        SetRotation(0);

        EmptyPot();
    }

    public void EmptyPot()
    {
        ingredientsInPot.Clear();
        stir.ResetPot();
    }

    void ShowText(string newText)
    {
        text.text = "You Made " + newText;
    }

    void TextDisappear()
    {
        text.text = "";
    }

    public void RotationComplete()
    {
        SetRotation(rotations + 1);
    }

    public void StoppedStirring()
    { 
        if(ingredientsInPot.Count <= 0)
        {
            rotations = 0;
            stir.ResetPot();
        }
    }

    public void SetRotation(int rotation)
    {
        rotations = rotation;
        rotationNumber.text = $"{rotations}";
    }
}