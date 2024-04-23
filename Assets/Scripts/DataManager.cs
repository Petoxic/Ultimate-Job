using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    // game constants
    private const int startingMoney = 20;
    public const int totalCases = 2;
    // number of days
    public static readonly int[] totalDays = new int[] { 3, 3 };
    // (profit, #talks, #serves) objetives for each day of each case
    private static readonly (int, int, int)[][] objectives = new (int, int, int)[][] {
        new (int, int, int)[] { (20, 2, 2), (30, 3, 3), (40, 4, 4) },
        new (int, int, int)[] { (20, 2, 2), (30, 3, 3), (40, 4, 4) }
    };
    // correct suspect name for each case
    public static readonly string[] suspects = new string[totalCases] { "Mr. Oliver Ford - Suspicious-looking man", "Mr. David Chase - Bank manager" };
    public const float cellSize = 0.16f;
    public const float dayTimeLimit = 45f;

    // game state variables
    // used throughout the game
    private static int totalMoney;
    public static int previousCaseStartingMoney; //
    private static string moneyText;
    public static bool startTalking;
    public static int caseNumber;
    public static string selectedSuspectName;

    // for each case
    private static int day;
    public static HashSet<string> suspectList;
    public static Dictionary<int, List<Vector2Int>> placedObjectsData;
    public static Dictionary<Vector3Int, PlacementData> placedObjects;
    public static List<GameObject> placedGameObjects;
    public static List<int> customerOrders;
    public static int[] customerQueue;
    public static int queuePos;
    public static Vector3 playerPosition;

    // for each day
    private static bool isDayEnded;
    public static bool[] isObjectiveCompleted;
    private static int todayProfit;
    private static int todayTalked;
    private static int todayServed;
    public static bool isDay;

    // game stats
    public static int moveLevels;
    public static int cookingLevels;

    void Start()
    {
        ResetGameData();

        DontDestroyOnLoad(gameObject);
    }

    private static void ResetGameData()
    {
        SetTotalMoney(startingMoney);
        previousCaseStartingMoney = startingMoney;
        startTalking = false;
        caseNumber = 0;
        moveLevels = 1;
        cookingLevels = 1;

        selectedSuspectName = "";

        // reset case data
        ResetCaseData();
    }

    private static void ResetCaseData()
    {
        day = 0;
        suspectList = new HashSet<string>();
        placedObjectsData = new Dictionary<int, List<Vector2Int>> { };
        placedObjects = new();
        placedGameObjects = new();
        customerOrders = Enumerable.Range(1, CustomerData.GetCustomerData().Count).ToList();
        customerQueue = Utils.ShuffleArray(customerOrders);
        queuePos = 0;
        playerPosition = new Vector3(0, 0, 0);

        // reset day data
        ResetDayData();
    }

    private static void ResetDayData()
    {
        isDayEnded = false;
        isObjectiveCompleted = new bool[] { false, false, false };
        todayProfit = 0;
        todayTalked = 0;
        todayServed = 0;
        isDay = false;
    }

    public static void AddMoney(int money)
    {
        SetTotalMoney(totalMoney + money);
        todayProfit += money;
    }

    public static void SubMoney(int money)
    {
        SetTotalMoney(totalMoney - money);
    }

    public static void Refund(int money)
    {
        SetTotalMoney(totalMoney + money);
    }

    public static void Reward(int money)
    {
        SetTotalMoney(totalMoney + money);
    }

    public static void AddSuspectList(string suspect)
    {
        if (!suspectList.Contains(suspect))
        {
            todayTalked += 1;
        }
        suspectList.Add(suspect);
    }

    public static void AddPlateServed()
    {
        todayServed += 1;
    }

    public static void CheckObjective()
    {
        if (todayProfit >= GetProfitObjective())
        {
            isObjectiveCompleted[0] = true;
        }
        if (todayTalked >= GetTalkedObjective())
        {
            isObjectiveCompleted[1] = true;
        }
        if (todayServed >= GetServedObjective())
        {
            isObjectiveCompleted[2] = true;
        }
    }

    public static void RestartGame()
    {
        ResetGameData();
    }

    public static bool CheckSuspect()
    {
        return suspects[caseNumber] == selectedSuspectName;
    }

    public static void NextDay()
    {
        day += 1;
        ResetDayData();
        SceneManager.LoadScene("NightScene");
    }

    public static void NextCase()
    {
        if (IsLastCase())
        {
            ResetGameData();
            SceneManager.LoadScene("GameOver");
            return;
        }

        caseNumber += 1;
        ResetCaseData();
        SceneManager.LoadScene("NightScene");
    }

    public static bool IsLastCase()
    {
        return caseNumber == totalCases - 1;
    }

    public static int GetDay()
    {
        return day;
    }

    public static int GetCaseNumber()
    {
        return caseNumber;
    }

    public static int GetTodayProfit()
    {
        return todayProfit;
    }

    public static int GetTodayTalked()
    {
        return todayTalked;
    }

    public static int GetTodayServed()
    {
        return todayServed;
    }

    public static int GetProfitObjective()
    {
        return objectives[caseNumber][day].Item1;
    }

    public static int GetTalkedObjective()
    {
        return objectives[caseNumber][day].Item2;
    }

    public static int GetServedObjective()
    {
        return objectives[caseNumber][day].Item3;
    }

    public static bool IsDayEnded()
    {
        return isDayEnded;
    }

    public static void SetDayEnded(bool value)
    {
        isDayEnded = value;
    }

    public static int GetTotalMoney()
    {
        return totalMoney;
    }

    private static void SetTotalMoney(int value)
    {
        totalMoney = value;
        moneyText = $"$ {totalMoney}";
    }

    public static string GetMoneyText()
    {
        return moneyText;
    }

    public static int GetCustomerQueue()
    {
        return customerQueue[queuePos++];
    }

    public static void ResetCase()
    {
        SetTotalMoney(previousCaseStartingMoney);
        ResetCaseData();
        SceneManager.LoadScene("NightScene");
    }

    public static void SetPreviousCaseStartingMoney(int money)
    {
        previousCaseStartingMoney = money;
    }
}
