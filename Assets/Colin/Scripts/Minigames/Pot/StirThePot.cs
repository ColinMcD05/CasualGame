using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class StirThePot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Camera thisCamera;
    [SerializeField] float rotationAmount = 2;
    CookingPot pot;

    Vector2 center;
    Vector2 previousVector;
    float previousSlope;
    float angle = 0;

    bool rotationDone = false;

    void Start()
    {
        pot = GetComponent<CookingPot>();
        center = transform.position;
        print(center);
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        previousVector = eventData.position - center;
        previousVector = previousVector.normalized;
        angle = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rotationDone) return;
        Vector2 newVector = eventData.position - center;
        newVector = newVector.normalized;

        float addedAngle = Vector2.SignedAngle(newVector, previousVector);

        angle += addedAngle;
        previousVector = newVector;

        if(angle/360 > rotationAmount)
        {
            pot.MakeMeal();
            rotationDone = true;
        }

        print(angle);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        angle = 0;
        rotationDone = false;
    }
}
