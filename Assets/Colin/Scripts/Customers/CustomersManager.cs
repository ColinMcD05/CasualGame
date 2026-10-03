using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    [SerializeField] private int customerLimit;
    [SerializeField] private Transform[] orderPositions;
    [SerializeField] private Transform[] waitingPositions;
    [SerializeField] private TextMeshProUGUI orderText;

    List<CustomerOrder> needToTake = new();
    List<CustomerOrder> alreadyTaken = new();

    [SerializeField] private GameObject[] customerPrefabs;

    public void Start()
    {
        InvokeRepeating("AddNewCustomer", 1, Random.Range(8, 15));
    }

    public void TakeOrder()
    {
        alreadyTaken.Add(needToTake[0]);
        orderText.text = needToTake[0].GetOrder().recipeName;

        Invoke("OrderTaken", 2f);
    }

    public void OrderTaken()
    {
        needToTake[0].transform.position = waitingPositions[alreadyTaken.Count - 1].position;
        orderText.text = "";

        needToTake.RemoveAt(0);

        for(int i = 0; i < needToTake.Count; i++)
        {
            needToTake[i].transform.position = orderPositions[i].position;
        }
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

        for (int i = 0; i < alreadyTaken.Count; i++)
        {
            alreadyTaken[i].transform.position = waitingPositions[i].position;
        }
    }

    public bool CheckCustomersToOrder()
    {
        return needToTake.Count > 0;
    }

    public bool CheckCustomersToGive()
    {
        return alreadyTaken.Count > 0;
    }

    public void AddNewCustomer()
    {
        if (needToTake.Count + alreadyTaken.Count < customerLimit)
        {
            int randomIndex = Random.Range(0, customerPrefabs.Length);
            GameObject newCustomer = Instantiate(customerPrefabs[randomIndex], orderPositions[needToTake.Count]);
            newCustomer.transform.position = orderPositions[needToTake.Count].position;

            needToTake.Add(newCustomer.GetComponent<CustomerOrder>());
        }
    }

    public int GetNeedToTakeCount()
    {
        return needToTake.Count;
    }

    public int GetTakenCount()
    {
        return alreadyTaken.Count;
    }

    public Recipe GetRecipeByIndex(int index)
    {
        if(index >= alreadyTaken.Count)
        {
            return null;
        }
        return alreadyTaken[index].GetOrder();
    }
}