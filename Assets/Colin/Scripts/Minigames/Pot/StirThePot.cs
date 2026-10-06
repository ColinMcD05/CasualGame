using UnityEngine;
using UnityEngine.EventSystems;

public class StirThePot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector2 previousVector;
    float angle = 0;

    public void OnPointerDown(PointerEventData eventData) 
    {
        previousVector = eventData.position;
        angle = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float dot = Vector2.Dot(previousVector, eventData.position);

        float magMult = previousVector.magnitude * eventData.position.magnitude;

        float addedAngle = Mathf.Acos(dot / magMult) * Mathf.Rad2Deg;

        angle += addedAngle;

        Debug.Log($"{previousVector}, {eventData.position}, {angle}");

        previousVector = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        angle = 0;
    }
}
