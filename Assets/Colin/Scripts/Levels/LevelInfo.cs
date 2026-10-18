using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    int totalStars;

    public void IncreaseStarts(int amount)
    {
        totalStars += amount;
    }
}
