using TMPro;
using UnityEngine;

public class ShowOrders : MonoBehaviour
{
    [SerializeField] CanvasGroup group;
    [SerializeField] CanvasGroup[] groups;
    [SerializeField] CustomersManager customersManager;

    public void Activate()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;

        for (int i = 0; i < groups.Length; i++)
        {
            if(i < customersManager.GetTakenCount())
            {
                groups[i].alpha = 1;
                Recipe recipe = customersManager.GetRecipeByIndex(i);
                TextMeshProUGUI[] text = groups[i].GetComponentsInChildren<TextMeshProUGUI>();
                text[0].text = recipe.recipeName;
                for(int j = 1; j < text.Length; j++)
                {
                    if (j - 1 < recipe.ingredients.Length)
                    {
                        text[j].text = recipe.ingredients[j - 1].GetIngredientName();
                    }
                    else
                    {
                        text[j].text = "";
                    }
                }
            }
            else
            {
                groups[i].alpha = 0;
            }
        }
    }

    public void Deactivate()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
    }
}
