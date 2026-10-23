using UnityEngine;

public class Transitions : MonoBehaviour
{
    [SerializeField] CameraType nextCamera;

    [SerializeField] CanvasGroup nextCanvasGroup;
    [SerializeField] CanvasGroup oldCanvasGroup;

    public void Transition()
    {
        CameraManager.SwapCurrentCamera(nextCamera);

        oldCanvasGroup.alpha = 0;
        oldCanvasGroup.interactable = false;
        oldCanvasGroup.blocksRaycasts = false;

        nextCanvasGroup.alpha = 1;
        nextCanvasGroup.interactable = true;
        nextCanvasGroup.blocksRaycasts = true;
    }
}
