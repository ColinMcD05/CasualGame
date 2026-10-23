using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
    public delegate void CameraChanged(Camera camera);
    public static event CameraChanged OnCameraChanged;

    [SerializeField] static Camera restaurantCamera, potCamera, enemyCamera;
    static Camera currentCamera;

    private void Awake()
    {
        restaurantCamera = GameObject.FindGameObjectWithTag("RestaurantCamera").GetComponent<Camera>();
        potCamera = GameObject.FindGameObjectWithTag("CauldronCamera").GetComponent<Camera>();
        enemyCamera = GameObject.FindGameObjectWithTag("EnemyCamera").GetComponent<Camera>();

        //Setup cameras
        currentCamera = restaurantCamera;
        currentCamera.enabled = true;

        potCamera.enabled = false;
        enemyCamera.enabled = false;
    }

    //Swaps the current camera with the new camera.
    public static void SwapCurrentCamera(CameraType cameraType)
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

    public static Camera GetCameraOfType(CameraType cameraType)
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