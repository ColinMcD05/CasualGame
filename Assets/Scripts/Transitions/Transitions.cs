using UnityEngine;

public class Transitions : MonoBehaviour
{
    [SerializeField] CameraType nextCamera;

    [SerializeField] UIType nextCanvasGroup;

    public void Transition()
    {
        CameraManager.SwapCurrentCamera(nextCamera);

        UIManager.SwapCurrentCanvasGroup(nextCanvasGroup);
    }
}
