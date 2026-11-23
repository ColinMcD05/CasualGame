using UnityEngine;

public class AnalogTimer : MonoBehaviour
{
    [SerializeField] CustomerOrder customerOrder;
    [SerializeField] SpriteRenderer render;

    public RectTransform minuteHand;
    const float minutesToDegrees = 360 / 90;

    // Update is called once per frame
    void Update()
    {
        if (customerOrder == null)
            return;

        if (customerOrder.GetCanTake())
        {
            render.enabled = true;
            minuteHand.rotation = Quaternion.Euler(0, 0, -customerOrder.GetSeconds() * minutesToDegrees);
        }
    }
}
