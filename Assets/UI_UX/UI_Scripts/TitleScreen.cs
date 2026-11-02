using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] CanvasGroup titleScreen;
    [SerializeField] CanvasGroup levelScreen;

    public enum Scenes
    {
        One,
        Two,
        Three
    }

    //called when start is pressed
    public void StartClicked()
    {
        //enable level select
        titleScreen.alpha = 0;
        titleScreen.interactable = false;
        titleScreen.blocksRaycasts = false;

        levelScreen.alpha = 1;
        levelScreen.interactable = true;
        levelScreen.blocksRaycasts = true;
    }

    public void LevelOneClicked(int day)
    {
        GameObject.FindFirstObjectByType<GameManager>().SetCurrentDay(1, day);
        GoToScene(Scenes.One);
    }
    public void LevelTwoClicked(int day)
    {
        GameObject.FindFirstObjectByType<GameManager>().SetCurrentDay(2, day);
        GoToScene(Scenes.Two);
    }

    public void LevelThreeClicked(int day)
    {
        GameObject.FindFirstObjectByType<GameManager>().SetCurrentDay(3, day);
        GoToScene(Scenes.Three);
    }

    private void GoToScene(Scenes scene)
    {

        switch(scene)
        { 
            case Scenes.One:
                SceneManager.LoadScene("Restuarant1");
                break;
            case Scenes.Two:
                SceneManager.LoadScene("Restaurant2");
                break;
            case Scenes.Three:
                SceneManager.LoadScene("Restaurant3");
                break;
            default:
                Debug.Log("How did this happen?");
                break;

        }
    }

}
