using UnityEngine;

public class Transitions : MonoBehaviour
{
    [SerializeField] Camera nextCamera;
    [SerializeField] Camera oldCamera;
    [SerializeField] CanvasGroup nextCanvasGroup;
    [SerializeField] CanvasGroup oldCanvasGroup;

    public void Transition()
    {
        oldCamera.enabled = false;
        nextCamera.enabled = true;

        oldCanvasGroup.alpha = 0;
        oldCanvasGroup.interactable = false;
        oldCanvasGroup.blocksRaycasts = false;

        nextCanvasGroup.alpha = 1;
        nextCanvasGroup.interactable = true;
        nextCanvasGroup.blocksRaycasts = true;
    }
}
