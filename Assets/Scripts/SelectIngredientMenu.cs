using UnityEngine;
using UnityEngine.EventSystems;

public class SelectIngredientMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup ingredientSelectScreen;
    [SerializeField] EnemyManager enemyManager;
    bool isOpened = true;
    public void ToggleMenu()
    {
        if (isOpened)
        {
            ingredientSelectScreen.alpha = 0;
            ingredientSelectScreen.interactable = false;
            ingredientSelectScreen.blocksRaycasts = false;
            isOpened = false;
        }

        else
        {
            ingredientSelectScreen.alpha = 1;
            ingredientSelectScreen.interactable = true;
            ingredientSelectScreen.blocksRaycasts = true;
            isOpened = true;
        }
    }

    public void SelectIngredient()
    {
        if(EventSystem.current.currentSelectedGameObject.TryGetComponent<Ingredients>(out Ingredients ingredient))
        {
            enemyManager.selectedIngredient = ingredient;
        }
        enemyManager.SpawnEnemy();
        ToggleMenu();
    }
}
