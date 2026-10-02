using System.Collections.Generic;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    [SerializeField] private int customerLimit;
    [SerializeField] private Transform[] orderPositions;
    [SerializeField] private Transform[] waitingPositions;

    List<CustomerOrder> needToTake;
    List<CustomerOrder> alreadyTaken;

    [SerializeField] private GameObject[] customerPrefabs;

    public void Start()
    {
        InvokeRepeating("AddNewCustomer", 2, Random.Range(8, 15));
    }

    public void OrderTaken()
    {
        alreadyTaken[0].transform.position = waitingPositions[needToTake.Count].position;

        alreadyTaken.Add(needToTake[0]);
        needToTake.RemoveAt(0);
    }

    public void OrderGiven(Recipe recipe)
    {
        alreadyTaken[0].TakeMeal(recipe);

        Invoke("RemoveCustomer", 2);
    }

    //Remove customer
    void RemoveCustomer()
    {
        GameObject remove = alreadyTaken[0].gameObject;
        Destroy(remove);

        if(alreadyTaken.Count == 0 && needToTake.Count == 0)
        {
            AddNewCustomer();   
        }
    }

    public bool CheckCustomersToOrder()
    {
        return needToTake.Count > 0;
    }

    public void AddNewCustomer()
    {
        if (needToTake.Count + alreadyTaken.Count < customerLimit)
        {
            int randomIndex = Random.Range(0, customerPrefabs.Length);
            GameObject newCustomer = Instantiate(customerPrefabs[randomIndex], orderPositions[needToTake.Count]);

            needToTake.Add(newCustomer.GetComponent<CustomerOrder>());
        }
    }
}