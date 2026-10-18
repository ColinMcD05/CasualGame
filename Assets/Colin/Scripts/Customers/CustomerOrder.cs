using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour
{
	public bool boss;
	private Menu menu;
	private Recipe order;
	private Shelves shelf;
	private Image indicator;
	[SerializeField] Sprite goodSprite, badSprite;
	[SerializeField] AudioClip correctOrderGiven, incorrectOrderGiven;
    private AudioSource giveOrderSource;

	[SerializeField] float ehTime = 30, madTime = 60;

	private float waitTimer = 0;


    void Awake()
	{
		menu = GameObject.FindFirstObjectByType<Menu>();

		indicator = GameObject.Find("Indicator").GetComponent<Image>();
		giveOrderSource = gameObject.AddComponent<AudioSource>();
    }

	void Start()
	{
		order = menu.GetRandomRecipe();
	}

    public CustomerSatisfaction TakeMeal(Recipe meal)
	{
		if(meal == null || meal.recipeName != order.recipeName)
		{
			BadOrder();
			return CustomerSatisfaction.Unsatisfied;
        }
		else if (waitTimer > madTime)
		{
			BadOrder();
			return CustomerSatisfaction.Mad;
		}
		else if (waitTimer > ehTime)
		{
			MehOrder();
			return CustomerSatisfaction.Meh;
		}
		else
		{
			GoodOrder();
			return CustomerSatisfaction.Satisfied;
		}
	}

	public void SetOrder()
	{
		menu.GetRandomRecipe();
	}

	public Recipe GetOrder()
	{
		return order;
	}

	public void GoodOrder()
	{
        Color newColor = Color.white;
        newColor.a = 1;
        indicator.sprite = goodSprite;
		indicator.color = newColor;
		giveOrderSource.PlayOneShot(correctOrderGiven);


        Invoke("DisappearIndicator", 2);
    }

	public void MehOrder()
	{
        Color newColor = Color.white;
        newColor.a = 1;
        indicator.sprite = goodSprite;
        indicator.color = newColor;
        giveOrderSource.PlayOneShot(correctOrderGiven);


        Invoke("DisappearIndicator", 2);
    }

	public void BadOrder()
	{
        Color newColor = Color.white;
        newColor.a = 1;
        indicator.sprite = badSprite;
		indicator.color = newColor;
		giveOrderSource.PlayOneShot(incorrectOrderGiven);

        Invoke("DisappearIndicator", 2);
    }

	public void DisappearIndicator()
	{
		Color newColor = Color.white;
		newColor.a = 0;
		indicator.color = newColor;
	}

	public IEnumerator IncreaseWaitTimer()
	{
		while(waitTimer <= madTime)
		{
			waitTimer += Time.deltaTime;
			yield return null;
		}
	}
}
