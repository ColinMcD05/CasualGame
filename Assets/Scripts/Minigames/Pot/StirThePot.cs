using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class StirThePot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Camera thisCamera;
    [SerializeField] Animation stirringAnimation;
    GameObject potObject;
    [SerializeField] AudioClip stirringClip;
    CookingPot pot;
    AudioSource source;
    float dragDepth;

    //Inputs
    MobileActions mobileActions;
    InputAction touch;
    InputAction drag;

    Vector2 center;
    Vector2 previousVector;
    float previousSlope;
    float angle = 0;

    void Start()
    {
        pot = GetComponent<CookingPot>();
        center = transform.position;

        source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = stirringClip;

        mobileActions = new();
        touch = mobileActions.MobileAction.Touch;
        touch = mobileActions.MobileAction.Tracking;
    }

    void OnEnable()
    {
        touch.Enable();
        touch.performed += CheckIfOverObject;

        drag.Enable();
        drag.performed += Stirring;
    }

    void OnDisable()
    {
        touch.performed -= CheckIfOverObject;
        touch.Disable();

        drag.performed -= Stirring;
        drag.Disable();
    }

    public void CheckIfOverObject(InputAction.CallbackContext context)
    {
        Vector2 screenPostion = context.ReadValue<Vector2>();
        Ray position = thisCamera.ScreenPointToRay(screenPostion);

        if (Physics.Raycast(position, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<StirThePot>())
            {
                potObject = hit.collider.gameObject;

                previousVector = screenPostion - center;
                previousVector = previousVector.normalized;
            }
            else
            {
                potObject = null;
            }
        }
        else
        {
            potObject = null;
        }
    }

    public void Stirring(InputAction.CallbackContext context)
    {
        if(potObject)
        {
            Vector2 newVector = context.ReadValue<Vector2>() - center;
            newVector = newVector.normalized;

            float addedAngle = Vector2.SignedAngle(newVector, previousVector);

            if (addedAngle <= 0)
            {
                addedAngle = Mathf.Abs(addedAngle);
                angle += addedAngle;


                if (angle >= 360)
                {
                    angle = angle - 360;
                    pot.RotationComplete();
                }

                if (!source.isPlaying)
                {
                    source.UnPause();
                }
            }
            else
            {
                source.Pause();
            }

            if (stirringAnimation)
            {
                stirringAnimation.clip.SampleAnimation(potObject, NextAnimationPosition(newVector));
            }

            previousVector = newVector;
        }
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        previousVector = eventData.position - center;
        previousVector = previousVector.normalized;

        if (stirringAnimation)
        {
            stirringAnimation.clip.SampleAnimation(potObject, NextAnimationPosition(previousVector));
        }
        source.Play();
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
            
            if (!source.isPlaying)
            {
                source.UnPause();
            }
        }
        else
        {
            source.Pause();
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
        source.Stop();
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
