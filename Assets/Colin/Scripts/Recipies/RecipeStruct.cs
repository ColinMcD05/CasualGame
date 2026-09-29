using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Recipe
{
    public string name;
    public List<Ingredients> ingredients = new();
    public GameObject mealPrefab;

    public Recipe()
    {
        if (ingredients.Count != 0)
        {
            ingredients.Sort();
        }
    }
}