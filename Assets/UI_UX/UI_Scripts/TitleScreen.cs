using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TitleScreen : MonoBehaviour
{
    public CanvasGroup titleGroup;

    public enum Scenes
    {
        One,
        Two,
        Three
    }

    //called when start is pressed
    public void StartClicked()
    {
        //go to main scene
        GoToScene(Scenes.One);
    }

    public void GoToScene(Scenes scene)
    {

        switch(scene)
        { 
            case Scenes.One:
                SceneManager.LoadScene("RestuarantOne");
                break;
            default:
                Debug.Log("How did this happen?");
                break;

        }
    }

}
