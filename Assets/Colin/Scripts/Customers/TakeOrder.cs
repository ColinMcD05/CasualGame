using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeOrder : MonoBehaviour
{
    public CustomersManager customersManager;
    public Button button;
    public TextMeshProUGUI text;

    void Start()
    {
        Invoke("ShowText", 0.5f);
    }

    void Update()
    {
        if (!button.enabled && customersManager.GetNeedToTakeCount() > 0)
        {
            button.enabled = true;
        }
        else if(button.enabled && customersManager.GetNeedToTakeCount() <= 0)
        {
            button.enabled = false;
        }
    }

    public void Order()
    {
        if (customersManager.CheckCustomersToOrder())
        {
            
            if(text.enabled)
            {
                text.enabled = false;
                customersManager.TakeOrder(true);
            }
            else
            {
                customersManager.TakeOrder(true);
            }
        }
    }

    public void ShowText()
    {
        text.enabled = true;
    }
}
