using UnityEngine;
using UnityEngine.EventSystems;

public class SelectIngredientMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup inventorySelectScreen;
    [SerializeField] EnemyManager enemyManager;
    bool isOpened = false;
    public void ToggleMenu()
    {
        if (isOpened)
        {
            inventorySelectScreen.alpha = 0;
            inventorySelectScreen.interactable = false;
            inventorySelectScreen.blocksRaycasts = false;
            isOpened = false;
        }

        else
        {
            inventorySelectScreen.alpha = 1;
            inventorySelectScreen.interactable = true;
            inventorySelectScreen.blocksRaycasts = true;
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
