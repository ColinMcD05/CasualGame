using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public const int hoursInADay = 24, minutesInAnHour = 60;
    public float dayDuration = 90f;

    float totalTime = 0;
    float currentTime = 0;

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        currentTime = totalTime % dayDuration;
    }

    public float GetHour()
    {
        return currentTime * hoursInADay / dayDuration;
    }

    public float GetMinutes()
    {
        return (currentTime * hoursInADay * minutesInAnHour / dayDuration) % minutesInAnHour;
    }
}
