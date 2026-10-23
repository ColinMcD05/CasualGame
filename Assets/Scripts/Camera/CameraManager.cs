using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
    public delegate void ChangeCamera(CameraType cameraType);
    public static event ChangeCamera OnChangeCamera;

    public delegate void CameraChanged(Camera camera);
    public static event CameraChanged OnCameraChanged;

    [SerializeField] Camera restaurantCamera, potCamera, enemyCamera;
    static Camera currentCamera;

    private void Awake()
    {
        //Setup cameras
        currentCamera = restaurantCamera;
        currentCamera.enabled = true;

        potCamera.enabled = false;
        enemyCamera.enabled = false;
    }

    private void OnEnable()
    {
        OnChangeCamera += SwapCurrentCamera;
    }

    private void OnDisable()
    {
        OnChangeCamera -= SwapCurrentCamera;
    }

    //Swaps the current camera with the new camera.
    public void SwapCurrentCamera(CameraType cameraType)
    {
        //Swaps camera
        switch(cameraType)
        {
            case CameraType.Restaurant:
                currentCamera.enabled = false;
                currentCamera = restaurantCamera;
                currentCamera.enabled = true;
                break;
            case CameraType.Pot:
                currentCamera.enabled = false;
                currentCamera = potCamera;
                currentCamera.enabled = true;
                break;
            case CameraType.Enemy:
                currentCamera.enabled = false;
                currentCamera = enemyCamera;
                currentCamera.enabled = true;
                break;
        }

        OnCameraChanged?.Invoke(currentCamera);
    }

    public Camera GetCameraOfType(CameraType cameraType)
    {
        switch(cameraType)
        {
            default:
            case CameraType.Restaurant:
                return restaurantCamera;
            case CameraType.Pot:
                return potCamera;
            case CameraType.Enemy:
                return enemyCamera;
        }
    }

    public static Camera GetCurrentCamera()
    {
        return currentCamera;
    }
}

public enum CameraType
{
    Restaurant,
    Pot,
    Enemy
}