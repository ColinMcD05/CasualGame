using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
	private Menu menu;
	private Recipe order;
	private Shelves shelf;

	void Awake()
	{
		menu = GameObject.FindFirstObjectByType<Menu>();
		if(menu)
		{
			order = menu.GetRandomRecipe();
		}
	}

	void Start()
	{
		order = menu.GetRandomRecipe();
	}

	public void TakeMeal(Recipe meal)
	{
		if(meal == null)
		{
			//Bad stuff happens
		}
		if(meal.recipeName == order.recipeName)
		{
			//Good stuff happens
		}
		shelf.RemoveFinishedMeal(meal);
	}

	public Recipe GetOrder()
	{
		return order;
	}
}
