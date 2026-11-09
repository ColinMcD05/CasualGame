using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    private float speed = 1f;
    public void SetSpeed(float incomingSpeed)
    {
        speed = incomingSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
