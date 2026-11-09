using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientContainers : MonoBehaviour, IDraggable3D
{
    [SerializeField] Vector2 maxZValues;

    Camera cauldronCamera;
    GameObject hitObject;
    [SerializeField] Transform movingObject;
    float dragDepth;

    Vector3 previousPosition;

    void Awake()
    {
        cauldronCamera = GameObject.FindGameObjectWithTag("CauldronCamera").GetComponent<Camera>();
    }

    public void StartDrag(Vector2 newScreenPosition)
    {
        Vector2 screenPostion = newScreenPosition;
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

    public void Drag(Vector2 screenPosition)
    {
        if (hitObject)
        {
            Vector2 newPosition = screenPosition;
            Vector3 toWorld = cauldronCamera.ScreenToWorldPoint(new Vector3(newPosition.x, newPosition.y, dragDepth));

            float distance = previousPosition.z - toWorld.z;
            previousPosition = toWorld;

            Vector3 newVector3 = movingObject.localPosition - Vector3.forward * distance/2;
            newVector3.z = Mathf.Clamp(newVector3.z, maxZValues.x, maxZValues.y);

            movingObject.localPosition = newVector3;
        }
    }
}
