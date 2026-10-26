using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void InventoryChange();
    public static event InventoryChange OnInventoryChange;

    //Variables
    [SerializeField] Dictionary<Ingredients, int> ingredients = new();

    [SerializeField] Ingredients[] initialIngredients;
    [SerializeField] int startingIngredientAmount;

    public void Awake()
    {
        if(ingredients.Count <= 0)
        {
            foreach(Ingredients ingredient in initialIngredients)
            {
                ingredients.Add(ingredient, startingIngredientAmount);
            }
        }
    }

    public void AddIngredient(Ingredients ingredient, int amount)
    {
        if (!ingredients.ContainsKey(ingredient))
        {
            ingredients.Add(ingredient, 0);
            Debug.Log("Not There");
        }

        ingredients[ingredient] += amount;
        OnInventoryChange?.Invoke();
    }

    public void RemoveIngredient(Ingredients ingredient)
    {
        if (!ingredients.ContainsKey(ingredient))
            return;

        if (ingredients[ingredient] <= 0)
            return;

        ingredients[ingredient] = Mathf.Max(--ingredients[ingredient], 0);
        Debug.Log($"{ingredient.name}: {ingredients[ingredient]}");

        OnInventoryChange?.Invoke();
    }

    public bool InInventory(Ingredients ingredient)
    {

        //Debug.Log($"{ingredient.name}: {!ingredients.ContainsKey(ingredient)}");

        if (!ingredients.ContainsKey(ingredient) || ingredients[ingredient] <= 0)
            return false;

        return true;
    }

    public Dictionary<Ingredients, int> GetInventory()
    {
        return ingredients;
    }
}
