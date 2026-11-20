using UnityEngine;
using UnityEngine.EventSystems;

public class SelectIngredientMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup ingredientSelectScreen;
    [SerializeField] CanvasGroup spellUI;
    [SerializeField] EnemyManager enemyManager;
    bool isOpened = true;
    public void ToggleMenu()
    {
        if (isOpened)
        {
            ingredientSelectScreen.alpha = 0;
            ingredientSelectScreen.interactable = false;
            ingredientSelectScreen.blocksRaycasts = false;
            spellUI.alpha = 1;
            spellUI.interactable = true;
            spellUI.blocksRaycasts = true;
            isOpened = false;
        }

        else
        {
            ingredientSelectScreen.alpha = 1;
            ingredientSelectScreen.interactable = true;
            ingredientSelectScreen.blocksRaycasts = true;
            spellUI.alpha = 0;
            spellUI.interactable = false;
            spellUI.blocksRaycasts = false;
            isOpened = true;
        }
    }

    public void SelectIngredient()
    {
        if(EventSystem.current.currentSelectedGameObject.TryGetComponent<IngredientsReference>(out IngredientsReference ingredient))
        {
            enemyManager.selectedIngredient = ingredient.GetIngredientReference();
        }
        enemyManager.SpawnEnemy();
        ToggleMenu();
    }
}
