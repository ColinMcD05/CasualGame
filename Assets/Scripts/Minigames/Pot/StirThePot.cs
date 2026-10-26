using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class StirThePot : MonoBehaviour, IDraggable3D
{
    Camera potCamera;
    [SerializeField] Animation stirringAnimation;
    [SerializeField] AudioClip stirringClip;
    CookingPot pot;
    AudioSource source;

    Vector2 center;
    Vector2 previousVector;
    float angle = 0;

    void Start()
    {
        pot = GetComponent<CookingPot>();
        potCamera = CameraManager.GetCameraOfType(CameraType.Pot);
        center = potCamera.WorldToScreenPoint(transform.position);

        source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = stirringClip;
    }

    public void StartDrag(Vector2 screenPosition)
    {
        previousVector = screenPosition - center;
        previousVector = previousVector.normalized;

        if (stirringAnimation)
        {
            stirringAnimation.clip.SampleAnimation(gameObject, NextAnimationPosition(previousVector));
        }
        source.Play();
    }

    public void Drag(Vector2 screenPosition)
    {
        Vector2 newVector = screenPosition - center;
        newVector = newVector.normalized;

        float addedAngle = Vector2.SignedAngle(newVector, previousVector);

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

        CancelInvoke();
        Invoke("StopSound", 0.1f);

        if (stirringAnimation)
        {
            stirringAnimation.clip.SampleAnimation(gameObject, NextAnimationPosition(newVector));
        }

        previousVector = newVector;
    }

    void StopSound()
    {
        source.Pause();
    }

    public void ResetPot()
    {
        angle = 0;
    }

    public float NextAnimationPosition(Vector2 position)
    {
        float angle = Vector2.SignedAngle(Vector2.right, position);
        if (angle < 0)
        {
            angle = 360 - Mathf.Abs(angle);
        }

        float nextPosition = angle * stirringAnimation.clip.length;

        nextPosition = nextPosition / 360;

        return nextPosition;
    }

}
