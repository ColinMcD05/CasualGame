using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TitleScreen : MonoBehaviour
{
    public CanvasGroup titleGroup, showOrdersGroup, orderWindowGroup;

    public enum Scenes
    {
        Main
    }

    //called when start is pressed
    public void StartClicked()
    {
        //go to main scene
        GoToScene(Scenes.Main);
    }

    public void GoToScene(Scenes scene)
    {

        switch(scene)
        { 
            case Scenes.Main:
                SceneManager.LoadScene("MainScene");
                break;
            default:
                Debug.Log("How did this happen?");
                break;

        }
    }

}
