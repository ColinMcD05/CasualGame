using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class StirThePot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Camera thisCamera;
    [SerializeField] Animation stirringAnimation;
    [SerializeField] GameObject potObject;
    CookingPot pot;

    Vector2 center;
    Vector2 previousVector;
    float previousSlope;
    float angle = 0;

    void Start()
    {
        pot = GetComponent<CookingPot>();
        center = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        previousVector = eventData.position - center;
        previousVector = previousVector.normalized;

        if (stirringAnimation)
        {
            stirringAnimation.clip.SampleAnimation(potObject, NextAnimationPosition(previousVector));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newVector = eventData.position - center;
        newVector = newVector.normalized;

        float addedAngle = Vector2.SignedAngle(newVector, previousVector);

        if(addedAngle <= 0)
        {
            addedAngle = Mathf.Abs(addedAngle);
            angle += addedAngle;


            if (angle >= 360)
            {
                angle = angle - 360;
                pot.RotationComplete();
            }
        }

        if (stirringAnimation)
        {
            stirringAnimation.clip.SampleAnimation(potObject, NextAnimationPosition(newVector));
        }

        previousVector = newVector;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pot.StoppedStirring();
    }

    public void ResetPot()
    {
        angle = 0;
    }

    public float NextAnimationPosition(Vector2 position)
    {
        float angle = Vector2.SignedAngle(Vector2.right, position);
        if(angle < 0)
        {
            angle = 360 - Mathf.Abs(angle);
        }

        float nextPosition = angle * stirringAnimation.clip.length;

        nextPosition = nextPosition / 360;

        return nextPosition;
    }
}
