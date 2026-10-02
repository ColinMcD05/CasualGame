using UnityEngine;
using System.Collections.Generic;

public class Shelves : MonoBehaviour
{
	//Variables
	[SerializeField] private Transform[] positions;

	private GameObject[] mealObjects;
	private List<Recipe> finishedMeals;

	void Awake()
	{
	}

	public void AddFinishedMeal(Recipe newMeal)
	{
		finishedMeals.Add(newMeal);
		UpdatePositions(newMeal);
	}

	public void RemoveFinishedMeal(Recipe mealToRemove)
	{
		finishedMeals.Remove(mealToRemove);
		UpdatePositions(mealToRemove);
	}

	private void UpdatePositions(Recipe newMeal)
	{
		foreach(GameObject meal in mealObjects)
		{
			Destroy(meal);
		}

		int count = Mathf.Min(positions.Length, finishedMeals.Count);

		for(int i = 0; i < count; i++)
		{
			GameObject meal = Instantiate(finishedMeals[i].mealPrefab, positions[i]);
			meal.GetComponent<FinishedMeal>().InitializeMeal(newMeal);

			mealObjects[i] = meal;
		}
	}
}