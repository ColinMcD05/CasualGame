using TMPro;
using UnityEngine;

public class ShowOrders : MonoBehaviour
{
    [SerializeField] CanvasGroup[] groups;
    [SerializeField] CustomersManager customersManager;

    public void Start()
    {
        customersManager = GameObject.FindAnyObjectByType<CustomersManager>();
    }

    public void UpdateOrders()
    {
        for (int i = 0; i < groups.Length; i++)
        {
            if (i < customersManager.GetTakenCount())
            {
                groups[i].alpha = 1;
                Recipe recipe = customersManager.GetRecipeByIndex(i);
                TextMeshProUGUI[] text = groups[i].GetComponentsInChildren<TextMeshProUGUI>();
                text[0].text = recipe.recipeName;
                for (int j = 1; j < text.Length; j++)
                {
                    if (j - 1 < recipe.ingredients.Length)
                    {
                        text[j].text = recipe.ingredients[j - 1].GetIngredientName();
                    }
                    else
                    {
                        text[j].text = "";
                    }
                    if (j == text.Length - 2)
                    {
                        text[j].text = $"{recipe.stirAmount}";
                    }
                    if (j == text.Length - 1)
                    {
                        text[j].text = $"Mix";
                    }
                }
            }
            else
            {
                groups[i].alpha = 0;
            }
        }
    }
}
