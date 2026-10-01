using UnityEngine;

public class Customer : MonoBehaviour
{
	private Menu menu;
	private Recipe order;

	private Shelves shelf;

	void Awake()
	{
		menu = GameObject.FindFirstObjectByType<Menu>();
	}

	void Start()
	{
		order = menu.GetRandomRecipe();
	}

	public void TakeMeal(Recipe meal)
	{
		if(meal = null)
		{
			//Bad stuff happens
		}
		if(meal.recipeName == order.recipeName)
		{
			//Good stuff happens
		}
		shelf.RemoveFinishedMeal(meal);
	}
}
