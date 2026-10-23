using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void InventoryChange();
    public static event InventoryChange OnInventoryChange;

    //Variables
    [SerializeField] Dictionary<Ingredients, int> ingredients = new();

    public void AddIngredient(Ingredients ingredient)
    {
        if (!ingredients.ContainsKey(ingredient))
        {
            ingredients.Add(ingredient, 0);
        }

        ingredients[ingredient]++;
        OnInventoryChange?.Invoke();
    }

    public void RemoveIngredient(Ingredients ingredient)
    {
        if (!ingredients.ContainsKey(ingredient))
            return;

        if (ingredients[ingredient] <= 0)
            return;

        ingredients[ingredient] = Mathf.Max(ingredients[ingredient]--, 0);
        OnInventoryChange?.Invoke();
    }

    public bool InInventory(Ingredients ingredient)
    {
        if (!ingredients.ContainsKey(ingredient) || ingredients[ingredient] <= 0)
            return false;

        return true;
    }

    public Dictionary<Ingredients, int> GetInventory()
    {
        return ingredients;
    }
}
