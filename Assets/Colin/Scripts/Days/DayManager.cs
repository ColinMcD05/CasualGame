using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManager : MonoBehaviour
{
    int currentDay = 1;
    [SerializeField] ShowDay showDay;
    [SerializeField] CustomersManager customersManager;

    [SerializeField] TextMeshProUGUI dayText;

    [SerializeField] Sprite fullStarSprite, noStarSprite;

    [SerializeField] Image[] images;
    public void Start()
    {
        DayStarted();
    }

    public void DayStarted()
    {
        dayText.enabled = true;
        dayText.text = $"Day: {currentDay}";

        Invoke("ResetUI", 1.5f); 
    }

    public void StartNewDay()
    {
        showDay.HideDay();
        customersManager.StartDay();
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
    }
}
