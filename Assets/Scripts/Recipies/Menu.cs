using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Menu : MonoBehaviour
{
    [SerializeField] private Recipe[] menu;
    [SerializeField] private Recipe bossRecipe;

    public Recipe CompareMenu(List<Ingredients> ingredients)
    {
        ingredients.Sort((a, b) => string.Compare(a.GetIngredientName(), b.GetIngredientName(), StringComparison.OrdinalIgnoreCase));
        int ingredientsCount = ingredients.Count();

        if (bossRecipe != null)
        {
            if (bossRecipe.ingredients.Length == ingredientsCount)
            {
                List<Ingredients> ingredientList = bossRecipe.ingredients.ToList();
                ingredientList.Sort((a, b) => string.Compare(a.GetIngredientName(), b.GetIngredientName(), StringComparison.OrdinalIgnoreCase));

                for (int i = 0; i < ingredientsCount; i++)
                {
                    if (ingredients[i].GetIngredientName() != ingredientList[i].GetIngredientName())
                    {
                        break;
                    }
                    else if (i == ingredientsCount - 1)
                    {
                        return bossRecipe;
                    }
                }
            }
        }

        if(menu.Length == 0)
        {
            return null;
        }

        foreach (Recipe recipe in menu)
        {
            if(recipe.ingredients.Length != ingredientsCount)
            {
                continue;
            }
            List<Ingredients> ingredientList = recipe.ingredients.ToList();
            ingredientList.Sort((a, b) => string.Compare(a.GetIngredientName(), b.GetIngredientName(), StringComparison.OrdinalIgnoreCase));
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

    public Recipe GetRandomRecipe()
    {
        int randomInt = UnityEngine.Random.Range(0, menu.Length);
        return menu[randomInt];
    }

    public Recipe GetBossRecipe()
    {
        return bossRecipe;
    }
}