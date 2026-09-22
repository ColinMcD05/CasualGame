using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Variables
    Dictionary<Ingredients, int> ingredients;

    public void AddToInventorty<T>(T newIngredient, int amount) where T : Ingredients
    {
        if(ingredients.ContainsKey(newIngredient))
        {
            ingredients[newIngredient] += amount;
        }
        else 
        {
            ingredients.Add(newIngredient, amount);
        }
    }

    public void RemoveFromInventory<T>(T removedIngredient, int amount) where T : Ingredients
    {
        if(!ingredients.ContainsKey(removedIngredient))
        {
            return;
        }
        if(ingredients[removedIngredient] < amount)
        {
            return;
        }

        ingredients[removedIngredient] -= amount;
    }

    public void GetIngredientByType<T>(Ingredients ingredients) where T : Ingredients
    {
        
    }
}
