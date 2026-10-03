using UnityEngine;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour
{
	private Menu menu;
	private Recipe order;
	private Shelves shelf;

	private Image indicator;

	void Awake()
	{
		menu = GameObject.FindFirstObjectByType<Menu>();
		if(menu)
		{
			order = menu.GetRandomRecipe();
		}

		indicator = GameObject.Find("Indicator").GetComponent<Image>();
	}

	void Start()
	{
		order = menu.GetRandomRecipe();
	}

	public void TakeMeal(Recipe meal)
	{
		if(meal == null || meal.recipeName != order.recipeName)
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

	public void GoodOrder()
	{
        Color newColor = Color.green;
        newColor.a = 1;
        indicator.color = newColor;

		Invoke("DisappearIndicator", 2);
    }

	public void BadOrder()
	{
        Color newColor = Color.red;
        newColor.a = 1;
        indicator.color = newColor;

        Invoke("DisappearIndicator", 2);
    }

	public void DisappearIndicator()
	{
		Color newColor = Color.white;
		newColor.a = 0;
		indicator.color = newColor;
	}
}
