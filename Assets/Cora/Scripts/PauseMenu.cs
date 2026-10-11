using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup pauseScreen;
    bool isPaused = false;
    public void Pause()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            pauseScreen.alpha = 0;
            pauseScreen.interactable = false;
            pauseScreen.blocksRaycasts = false;
            isPaused = false;
        }

        else
        {
            Time.timeScale = 0;
            pauseScreen.alpha = 1;
            pauseScreen.interactable = true;
            pauseScreen.blocksRaycasts = true;
            isPaused = true;
        }
    }
}
