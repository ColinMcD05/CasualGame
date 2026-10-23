using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientContainers : MonoBehaviour
{
    [SerializeField] Vector2 maxZValues;

    MobileActions mobileAction;
    InputAction tracking;
    InputAction touch;
    Camera cauldronCamera;
    GameObject hitObject;
    [SerializeField] Transform movingObject;
    float dragDepth;

    Vector3 previousPosition;

    void Awake()
    {
        mobileAction = new MobileActions();

        tracking = mobileAction.MobileAction.Tracking;
        touch = mobileAction.MobileAction.Touch;

        cauldronCamera = GameObject.FindGameObjectWithTag("CauldronCamera").GetComponent<Camera>();
    }


    public void CheckIfOverObject(InputAction.CallbackContext context)
    {
        Vector2 screenPostion = context.ReadValue<Vector2>();
        Ray position = cauldronCamera.ScreenPointToRay(screenPostion);

        if (Physics.Raycast(position, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<IngredientContainers>())
            {
                hitObject = hit.collider.gameObject;
                dragDepth = cauldronCamera.WorldToScreenPoint(hit.point).z;
                previousPosition = cauldronCamera.ScreenToWorldPoint(new Vector3(screenPostion.x, screenPostion.y, dragDepth));
            }
            else
            {
                hitObject = null;
            }
        }
        else
        {
            hitObject = null;
        }
    }

    public void DragIngredientContainer(InputAction.CallbackContext context)
    {
        if (hitObject)
        {
            Vector2 newPosition = context.ReadValue<Vector2>();
            Vector3 toWorld = cauldronCamera.ScreenToWorldPoint(new Vector3(newPosition.x, newPosition.y, dragDepth));

            float distance = previousPosition.z - toWorld.z;
            previousPosition = toWorld;

            Vector3 newVector3 = movingObject.localPosition - Vector3.forward * distance/2;
            newVector3.z = Mathf.Clamp(newVector3.z, maxZValues.x, maxZValues.y);

            movingObject.localPosition = newVector3;
        }
    }
}
