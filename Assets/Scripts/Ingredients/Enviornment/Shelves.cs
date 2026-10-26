using UnityEngine;
using System.Collections.Generic;

public class Shelves : MonoBehaviour
{
	//Variables
	[SerializeField] private Transform[] positions;

	private GameObject[] mealObjects;
	public FinishedMealDragHandler[] dragHandlers;
	private List<Recipe> finishedMeals = new();

	public FinishedMealDragHandler meal;

    void Awake()
	{
		mealObjects = new GameObject[positions.Length];
	}

	public void AddFinishedMeal(Recipe newMeal)
	{
		finishedMeals.Add(newMeal);
		UpdatePositions();
	}

    public void AddFinishedMeal(Recipe newMeal, GameObject badMeal)
    {
		if(newMeal == null)
		{
			newMeal = new Recipe();
			newMeal.recipeName = "Bad Meal";
			newMeal.mealPrefab = badMeal;

        }

        finishedMeals.Add(newMeal);
        UpdatePositions();
    }

    public void RemoveFinishedMeal(Recipe mealToRemove)
	{
		finishedMeals.Remove(mealToRemove);
		UpdatePositions();
	}

    public void RemoveFinishedMeal(int index)
    {
        finishedMeals.RemoveAt(index);
        UpdatePositions();
    }

    public void UpdatePositions()
	{
		foreach(GameObject meal in mealObjects)
		{
			Destroy(meal);
		}

		foreach(FinishedMealDragHandler drag in dragHandlers)
		{
			drag.MealDespawned();
		}

		int count = Mathf.Min(positions.Length, finishedMeals.Count);

		for(int i = 0; i < count; i++)
		{
			GameObject meal = Instantiate(finishedMeals[i].mealPrefab, positions[i]);
			meal.GetComponent<FinishedMeal>().InitializeMeal(finishedMeals[i]);
			meal.transform.position = positions[i].position;

			dragHandlers[i].MealSpawned(meal);

			mealObjects[i] = meal;
		}
	}

	public GameObject GetObjectAtIndex(int index)
	{
		if(index >= mealObjects.Length)
		{
			return null;
		}
		else
		{
			return mealObjects[index];
		}
	}

	public Recipe GetRecipeAtIndex(int index)
	{
        if (index >= finishedMeals.Count)
        {
            return null;
        }
        else
        {
            return finishedMeals[index];
        }
    }	
}