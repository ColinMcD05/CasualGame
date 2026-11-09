using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    [SerializeField] private int customerLimit = 5;
    [SerializeField] private int maxCustomersToday = 5;
    [SerializeField] private Transform[] orderPositions;
    [SerializeField] private Transform[] waitingPositions;
    [SerializeField] private TextMeshProUGUI orderText;
    [SerializeField] private TextMeshProUGUI dropoffText;

    List<CustomerOrder> needToTake = new();
    List<CustomerOrder> alreadyTaken = new();

    [SerializeField] private GameObject[] customerPrefabs;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] AudioClip newCustomerClip, takeOrder;
    AudioSource newCustomerSource, orderTakenSource;

    int satisfiedCustomers;
    int unsatisfiedCustomers;
    int mehCustomers;
    int madCustomers;

    int points;
    int maxPoints;

    bool canTakeOrder = true;

    [SerializeField] float baseThreeStars=0.75f, baseTwoStars=0.5f, baseOneStar=0.30f;

    [SerializeField] DayManager dayManager;

    private ShowOrders showOrders;

    List<int> indexArray = new List<int>();

    public void Awake()
    {
        showOrders = GameObject.FindFirstObjectByType<ShowOrders>();
    }

    public void Start()
    {
        newCustomerSource = gameObject.AddComponent<AudioSource>();
        orderTakenSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartDay(int maxCustomersToday)
    {
        this.maxCustomersToday = maxCustomersToday;

        Invoke("AddNewCustomer", 1.2f);

        satisfiedCustomers = 0;
        unsatisfiedCustomers = 0;
        mehCustomers = 0;

        maxPoints = maxCustomersToday * 3;
    }

    public void TakeOrder(bool first = false)
    {
        orderTakenSource.PlayOneShot(takeOrder);
        if (canTakeOrder)
        {
            needToTake[0].StartCoroutine("IncreaseWaitTimer");
            alreadyTaken.Add(needToTake[0]);
            orderText.text = needToTake[0].GetOrder().recipeName;
            canTakeOrder = false;

            if (first)
            {
                dropoffText.enabled = true;
            }

            Invoke("OrderTaken", 2f);
        }
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

        canTakeOrder = true;

        showOrders.UpdateOrders();
    }

    public void OrderGiven(Recipe recipe, CustomerSatisfaction wasGood)
    {
        switch (wasGood)
        {
            case CustomerSatisfaction.Satisfied:
                satisfiedCustomers++;
                break;
            case CustomerSatisfaction.Meh:
                mehCustomers++;
                break;
            case CustomerSatisfaction.Unsatisfied:
                unsatisfiedCustomers++;
                break;
            case CustomerSatisfaction.Mad:
                madCustomers++;
                unsatisfiedCustomers++;
                break;
        }

        points += ((int)wasGood);

        if (dropoffText.enabled)
        {
            dropoffText.enabled = false;
        }

        indexArray.Add(alreadyTaken.FindIndex(order => order.GetOrder() == recipe));

        CheckDayEnd();
        Invoke("RemoveCustomer", 2);
    }

    //Remove customer
    void RemoveCustomer()
    {
        GameObject remove = alreadyTaken[indexArray[0]].gameObject;
        alreadyTaken.RemoveAt(indexArray[0]);
        indexArray.RemoveAt(0);
        Destroy(remove);

        if(alreadyTaken.Count == 0 && needToTake.Count == 0)
        {
            AddNewCustomer();   
        }

        for (int i = 0; i < alreadyTaken.Count; i++)
        {
            alreadyTaken[i].transform.position = waitingPositions[i].position;
        }

        showOrders.UpdateOrders();
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
        if (satisfiedCustomers + unsatisfiedCustomers + mehCustomers + alreadyTaken.Count + needToTake.Count < maxCustomersToday)
        {
            if (alreadyTaken.Count + needToTake.Count >= customerLimit)
            {
                Invoke("AddNewCustomer", 5);
                return;
            }

            GameObject newCustomer = null;

            if ((satisfiedCustomers + unsatisfiedCustomers + mehCustomers + alreadyTaken.Count + needToTake.Count + 1) % maxCustomersToday == 0 && dayManager.GetCurrentDay() == 5)
            {
                int randomIndex = Random.Range(0, customerPrefabs.Length);
                newCustomer = Instantiate(bossPrefab, orderPositions[needToTake.Count]);
                newCustomer.transform.position = orderPositions[needToTake.Count].position;

                newCustomer.GetComponent<CustomerOrder>().SetOrder(true);
            }
            else
            {
                int randomIndex = Random.Range(0, customerPrefabs.Length);
                newCustomer = Instantiate(customerPrefabs[randomIndex], orderPositions[needToTake.Count]);
                newCustomer.transform.position = orderPositions[needToTake.Count].position;

                newCustomer.GetComponent<CustomerOrder>().SetOrder();
            }

            newCustomerSource.PlayOneShot(newCustomerClip);

            needToTake.Add(newCustomer.GetComponent<CustomerOrder>());

            Invoke("AddNewCustomer", Random.Range(10, 15));
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

    public void CheckDayEnd()
    {
        if(satisfiedCustomers + unsatisfiedCustomers + mehCustomers >= maxCustomersToday)
        {
            dayManager.FinishDay(satisfiedCustomers, unsatisfiedCustomers, mehCustomers, CalculateStars());

            CancelInvoke("AddNewCustomer");
        }
    }

    int CalculateStars()
    {
        float percentage = (float)points / maxPoints;
        print(percentage);

        if(percentage >= Mathf.Clamp(baseThreeStars + (dayManager.GetCurrentDay() * 0.075f), baseThreeStars, 0.875f))
        {
            return 3;
        }
        else if(percentage >= Mathf.Clamp(baseTwoStars + (dayManager.GetCurrentDay() * 0.05f), baseThreeStars, 0.65f))
        {
            return 2;
        }
        else if(percentage >= Mathf.Clamp(baseOneStar + (dayManager.GetCurrentDay() * 0.052f), baseOneStar, 0.5f))
        {
            return 1;
        }
        return 0;
    }
}