using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public void Pressed() {
        SceneManager.LoadScene("MainSceneDupe");
    }
}
