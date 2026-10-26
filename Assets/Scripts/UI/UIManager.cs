using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] static CanvasGroup restaurantUI, potUI, enemyUI;
    static CanvasGroup currentCanvas;

    private void Awake()
    {
        restaurantUI = GameObject.FindGameObjectWithTag("RestaurantUI").GetComponent<CanvasGroup>();
        potUI = GameObject.FindGameObjectWithTag("CauldronUI").GetComponent<CanvasGroup>();
        enemyUI = GameObject.FindGameObjectWithTag("EnemyUI").GetComponent<CanvasGroup>();

        //Setup cameras
        currentCanvas = restaurantUI;
        currentCanvas.alpha = 1;
        currentCanvas.interactable = true;
        currentCanvas.blocksRaycasts = true;

        potUI.alpha = 0;
        potUI.interactable = false;
        potUI.blocksRaycasts = false;

        if (enemyUI)
        {
            enemyUI.alpha = 0;
            enemyUI.interactable = false;
            enemyUI.blocksRaycasts = false;
        }
    }

    //Swaps the current camera with the new camera.
    public static void SwapCurrentCanvasGroup(UIType cameraType)
    {
        currentCanvas.alpha = 0;
        currentCanvas.interactable = false;
        currentCanvas.blocksRaycasts = false;

        //Swaps camera
        switch (cameraType)
        {
            case UIType.Restaurant:
                currentCanvas = restaurantUI;

                currentCanvas.alpha = 1;
                currentCanvas.interactable = true;
                currentCanvas.blocksRaycasts = true;
                break;
            case UIType.Pot:
                currentCanvas = potUI;

                currentCanvas.alpha = 1;
                currentCanvas.interactable = true;
                currentCanvas.blocksRaycasts = true;
                break;
            case UIType.Enemy:
                currentCanvas = enemyUI;

                currentCanvas.alpha = 1;
                currentCanvas.interactable = true;
                currentCanvas.blocksRaycasts = true;
                break;
        }
    }

    public static CanvasGroup GetCanvasGroupOfType(UIType uiType)
    {
        switch(uiType)
        {
            default:
            case UIType.Restaurant:
                return restaurantUI;
            case UIType.Pot:
                return potUI;
            case UIType.Enemy:
                return enemyUI;
        }
    }

    public static CanvasGroup GetCurrentCamera()
    {
        return currentCanvas;
    }
}

public enum UIType
{
    Restaurant,
    Pot,
    Enemy
}