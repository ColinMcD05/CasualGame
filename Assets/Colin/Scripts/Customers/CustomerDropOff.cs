using UnityEngine;

public class CustomerDropOff : MonoBehaviour
{
    [SerializeField] CustomersManager customerManager;

    public void DropOff(Recipe recipe)
    {
        customerManager.OrderGiven(recipe);
    }
}
