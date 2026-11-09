using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDay : MonoBehaviour
{
    [SerializeField] CanvasGroup showDayEndGroup;


    [SerializeField] Button nextDayButton;
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI satisfied;
    [SerializeField] TextMeshProUGUI meh;
    [SerializeField] TextMeshProUGUI unsatisfied;

    public void DisplayDayEnd(int satisfiedAmount, int unsatisifedAmount, int mehAmount, int currentDay, int starAmount)
    {
        showDayEndGroup.alpha = 1;
        showDayEndGroup.blocksRaycasts = true;
        showDayEndGroup.interactable = true;

        if(currentDay >= 5 || starAmount <= 0)
        {
            nextDayButton.interactable = false;
        }

        day.text = $"Day: {currentDay}";
        unsatisfied.text = $"Unhappy: {unsatisifedAmount}";
        satisfied.text = $"Happy: {satisfiedAmount}";
        meh.text = $"Ok: {mehAmount}";
    }

    public void HideDay()
    {
        showDayEndGroup.alpha = 0;
        showDayEndGroup.blocksRaycasts = false;
        showDayEndGroup.interactable = false;
    }
}
