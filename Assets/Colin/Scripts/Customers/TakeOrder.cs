using UnityEngine;

public class TakeOrder : MonoBehaviour
{
    CustomersManager customersManager;

    public void Order()
    {
        if (customersManager.CheckCustomersToOrder())
        {
            customersManager.Invoke("OrderTaken", 2);
        }
    }
}
