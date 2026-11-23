using UnityEngine;

public class AnalogTimer : MonoBehaviour
{
    TimerManager tm;

    public RectTransform minuteHand;
    const float minutesToDegrees = 360 / 90;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = GameObject.FindAnyObjectByType<TimerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        minuteHand.rotation = Quaternion.Euler(0, 0, -tm.GetMinutes() * minutesToDegrees);
    }
}
