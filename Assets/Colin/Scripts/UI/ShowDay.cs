using TMPro;
using UnityEngine;

public class ShowDay : MonoBehaviour
{
    [SerializeField] CanvasGroup showDayEndGroup;

    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI satisfied;
    [SerializeField] TextMeshProUGUI meh;
    [SerializeField] TextMeshProUGUI unsatisfied;

    public void DisplayDayEnd(int satisfiedAmount, int unsatisifedAmount, int mehAmount, int currentDay)
    {
        showDayEndGroup.alpha = 1;
        showDayEndGroup.blocksRaycasts = true;
        showDayEndGroup.interactable = true;

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
