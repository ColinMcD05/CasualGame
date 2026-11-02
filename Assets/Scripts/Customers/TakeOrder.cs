using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeOrder : MonoBehaviour
{
    public CustomersManager customersManager;
    public Button button;
    public TextMeshProUGUI showInfo;

    void Start()
    {
        Invoke("ShowText", 1.1f);
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
            
            if(showInfo.enabled)
            {
                print("Hello");
                showInfo.enabled = false;
                showInfo.text = "";
                customersManager.TakeOrder(true);
            }
            else
            {
                customersManager.TakeOrder();
            }
        }
    }

    public void ShowText()
    {
        showInfo.enabled = true;
    }
}