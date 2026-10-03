using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class TakeOrder : MonoBehaviour
{
    public CustomersManager customersManager;
    public Button button;
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
            customersManager.TakeOrder();
        }
    }
}
