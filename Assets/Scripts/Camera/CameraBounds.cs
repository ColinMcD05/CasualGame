using System;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] Vector2 maxBound;
    [SerializeField] Vector2 minBound;

    public void ClampBound(ref Vector3 vector)
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(vector.x, minBound.x, maxBound.x);
        position.y = Mathf.Clamp(vector.y, minBound.y, maxBound.y);

        vector = position;
    }
}
