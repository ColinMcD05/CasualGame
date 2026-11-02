using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameObject instance;

    int totalStars;
    string currentLevelID;

    int currentIndex;
    Days days;
    DayInfo currentDay;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        SceneManager.sceneLoaded += CheckAtMainMenu;
        LoadGame();
    }

    public void RecheckTotalStars()
    {
        totalStars = 0;

        foreach (DayInfo day in days.days)
        {
            totalStars += day.earnedStars;
        }

        bool prevUnlock = true;

        foreach(DayInfo day in days.days)
        {
            if (day.unlocked)
            {
                prevUnlock = true;
                continue;
            }

            if(totalStars >= day.GetNeededStars() && (prevUnlock || day.GetDay() == 1))
            {
                day.Unlock();
                prevUnlock = true;
            }
            else
            {
                prevUnlock = false;
            }
        }
    }

    public void DayCompleted()
    {
        days.days[currentIndex].completed = true;
    }

    public void SetCurrentDay(int restaurant, int day)
    {
        currentIndex = (restaurant - 1) * 5 + day - 1;
        currentDay = days.days[currentIndex];
    }

    public void NextDay()
    {
        currentIndex++;
        currentDay = days.days[currentIndex];
    }

    public DayInfo GetDayInfo()
    {
        return currentDay;
    }

    public void SaveLevel(DayInfo dayInfo)
    {
        days.days[currentIndex] = dayInfo;
        SaveGame();
    }

    public void SaveGame()
    {
        string filePath = Application.persistentDataPath;

        if(!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        filePath += "/Day_Data.json";

        string dayString = JsonUtility.ToJson(days);
        using (StreamWriter stream = File.CreateText(filePath))
        {
            stream.WriteLine(dayString);
        }
    }

    public void LoadGame()
    {
        days = new Days();

        string filePath = Application.persistentDataPath;

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            return;
        }

        filePath += "/Day_Data.json";

        if(!File.Exists(filePath))
        {
            return;
        }

        using(StreamReader stream = new StreamReader(filePath))
        {
            var dayString = stream.ReadToEnd();
            var dayData = JsonUtility.FromJson<Days>(dayString);

            for(int i = 0; i < dayData.days.Count; i++)
            {
                days.days[i].earnedStars = dayData.days[i].earnedStars;
                days.days[i].completed = dayData.days[i].completed;
                days.days[i].unlocked = dayData.days[i].unlocked;
            }
        }
    }

    public void CheckAtMainMenu(Scene scene, LoadSceneMode scene1)
    {
        if(scene.name == "MainMenu")
        {
            print("Hello");
        }
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }
}


[System.Serializable]
public class DayInfo
{
    string dayID;
    int day;
    int customerAmount;
    int neededStars;

    public int earnedStars = 0;
    public bool completed = false;
    public bool unlocked = false;

    public DayInfo(string dayID, int day, int customerAmount, int neededStars)
    {
        this.dayID = dayID;
        this.day = day;
        this.customerAmount = customerAmount;
        this.neededStars = neededStars;
    }

    public int GetDay()
    {
        return day;
    }

    public int GetCustomerAmount()
    {
        return customerAmount;
    }

    public int GetNeededStars()
    {
        return neededStars;
    }

    public void Unlock()
    {
        unlocked = true;
    }
}

[System.Serializable]
public class GameInfo
{
    int totalStars;
}

public class Days
{
    public List<DayInfo> days;

    public Days()
    {
        days = new List<DayInfo>
        {
            new DayInfo("1-1", 1, 5, 0),
            new DayInfo("1-2", 2, 6, 0),
            new DayInfo("1-3", 3, 7, 0),
            new DayInfo("1-4", 4, 8, 0),
            new DayInfo("1-5", 5, 9, 0),
            new DayInfo("2-1", 1, 6, 8),
            new DayInfo("2-2", 2, 7, 10),
            new DayInfo("2-3", 3, 8, 12),
            new DayInfo("2-4", 4, 9, 14),
            new DayInfo("2-5", 5, 10, 16),
            new DayInfo("3-1", 1, 7, 18),
            new DayInfo("3-2", 2, 8, 20),
            new DayInfo("3-3", 3, 9, 22),
            new DayInfo("3-4", 4, 10, 24),
            new DayInfo("3-5", 5, 11, 26)//,
            //new DayInfo("Infinite", -1, -1)
        };

        days[0].Unlock();
    }
}