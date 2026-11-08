using UnityEngine;

public class CustomerDropOff : MonoBehaviour
{
    [SerializeField] CustomersManager customerManager;

    public void Start()
    {
        customerManager = GameObject.FindFirstObjectByType<CustomersManager>();
    }
    public bool DropOff(Recipe recipe)
    {
        if (customerManager.CheckCustomersToGive())
        {
            customerManager.OrderGiven(recipe);
            return true;
        }
        return false;
    }
}