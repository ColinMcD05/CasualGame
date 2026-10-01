using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Menu : MonoBehaviour
{
    [SerializeField] private Recipe[] menu;

    public Recipe CompareMenu(List<Ingredients> ingredients)
    {
        if(menu.Length == 0)
        {
            return null;
        }

        ingredients.Sort();
        int ingredientsCount = ingredients.Count();
        foreach (Recipe recipe in menu)
        {
            if(recipe.ingredients.Length != ingredientsCount)
            {
                continue;
            }
            List<Ingredients> ingredientList = recipe.ingredients.ToList();
            ingredientList.Sort();
            for(int i = 0; i < ingredientsCount; i++)
            {
                if (ingredients[i].GetIngredientName() != ingredientList[i].GetIngredientName())
                {
                    break;
                }
                else if(i == ingredientsCount - 1)
                {
                    return recipe;
                }
            }
        }
        return null;
    }
}