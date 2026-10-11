using System;
using UnityEngine;

[Serializable]
public class Recipe
{
    public string recipeName;
    public Ingredients[] ingredients;
    public int stirAmount;
    public GameObject mealPrefab;
}