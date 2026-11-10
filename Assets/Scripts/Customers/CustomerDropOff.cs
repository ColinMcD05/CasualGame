using UnityEngine;

public class CustomerDropOff : MonoBehaviour
{
    [SerializeField] CustomersManager customerManager;
    [SerializeField] CustomerOrder order;

    public void Start()
    {
        customerManager = GameObject.FindFirstObjectByType<CustomersManager>();
    }
    public bool DropOff(Recipe recipe)
    {
        if(order.GetOrder() == null || order.GetTaken())
        {
            return false;
        }

        customerManager.OrderGiven(order.GetOrder(), order.TakeMeal(recipe));
        return true;
    }
}