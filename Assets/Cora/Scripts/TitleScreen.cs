using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        //title screen disappears
        titleGroup.alpha = 0;
        titleGroup.interactable = false;

        //go to main scene
        //StartCoroutine(GoToScene(Scenes.Main));

        showOrdersGroup.alpha = 1;
        showOrdersGroup.interactable = true;

        orderWindowGroup.alpha = 1;
        orderWindowGroup.interactable = true;
    }

    /*
        public IEnumerator GoToScene(Scenes scene)
    {


            case Scenes.Main:
            SceneManager.LoadScene("MainScene");
            break;
        default:
            Debug.Log("How did this happen?");
            break;

        }
    }
    */

}
