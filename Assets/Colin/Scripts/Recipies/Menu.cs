using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Menu : MonoBehaviour
{
    [SerializeField] private Recipe[] menu;

    public bool CompareMenu(List<Ingredients> ingredients)
    {
        if(menu.Length == 0)
        {
            return false;
        }

        ingredients.Sort();

        foreach (Recipe recipe in menu)
        {
            if(recipe.ingredients.Count() != ingredients.Count())
            {
                continue;
            }
            if (ingredients.Equals(recipe.ingredients))
            {
                return true;
            }
        }
        return false;
    }
}