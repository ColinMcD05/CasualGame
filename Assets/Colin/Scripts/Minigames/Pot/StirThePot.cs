using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StirThePot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData) 
    {
        print("Hello");
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
