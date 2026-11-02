using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManager : MonoBehaviour
{
    GameManager gameManager;
    DayInfo dayInfo;

    int currentDay = 1;
    [SerializeField] ShowDay showDay;
    [SerializeField] CustomersManager customersManager;

    [SerializeField] TextMeshProUGUI dayText;

    [SerializeField] Sprite fullStarSprite, noStarSprite;

    [SerializeField] Image[] images;
    public void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        dayInfo = gameManager.GetDayInfo();
        currentDay = dayInfo.GetDay();

        DayStarted();
    }

    public void DayStarted()
    {
        dayText.enabled = true;
        dayText.text = $"Day: {currentDay}";

        Invoke("ResetUI", 1.5f);

        customersManager.StartDay(dayInfo.GetCustomerAmount());
    }

    public void StartNewDay()
    {
        showDay.HideDay();
        gameManager.NextDay();
        dayInfo = gameManager.GetDayInfo();
        currentDay = dayInfo.GetDay();

        DayStarted();
    }

    public void ResetUI()
    {
        dayText.enabled = false;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }

    public void FinishDay(int satisfiedAmount, int unsatisifedAmount, int mehAmount, int starAmount)
    {
        showDay.DisplayDayEnd(satisfiedAmount, unsatisifedAmount, mehAmount, currentDay);
        currentDay++;

        for(int i = 0; i < images.Length; i++)
        {
            if(i <= starAmount-1)
            {
                images[i].sprite = fullStarSprite;
            }
            else
            {
                images[i].sprite = noStarSprite;
            }
        }

        gameManager.DayCompleted();
        dayInfo.earnedStars = starAmount;
        gameManager.SaveLevel(dayInfo);
    }
}
