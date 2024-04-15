using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    // Game settings
    private const int startingMoney = 20;
    public const int totalCases = 2;
    // number of days
    public static readonly int[] totalDays = new int[] { 3, 3 };
    // (profit, #talks, #serves) for each day
    private static readonly (int, int, int)[][] objectives = new (int, int, int)[][] {
        new (int, int, int)[] { (20, 2, 2), (30, 3, 3), (40, 4, 4) },
        new (int, int, int)[] { (20, 2, 2), (30, 3, 3), (40, 4, 4) }
    };
    // correct suspect name for each case
    public static readonly string[] suspects = new string[totalCases] { "Mr. Oliver Ford - Suspicious-looking man", "" };
    public static int totalMoney;
    public static string moneyText;
    public static HashSet<string> suspectList;
    public static bool[] isObjectiveCompleted;
    private static int day;
    private static int caseNumber;
    private static int todayProfit;
    private static int todayTalked;
    private static int todayServed;
    public static bool isGameEnd = false;
    public static bool startTalking;
    public static float cellSize;
    public static HashSet<string> appearedCharactors;
    public static Dictionary<int, List<Vector2Int>> placedObjectsData;
    void Start()
    {
        totalMoney = startingMoney;
        UpdateMoneyText();
        appearedCharactors = new HashSet<string>();
        suspectList = new HashSet<string>();
        isObjectiveCompleted = new bool[] { false, false, false };
        todayServed = 0;
        day = 0;
        caseNumber = 0;
        todayTalked = 0;
        startTalking = false;
        cellSize = 0.16f;
        placedObjectsData = new Dictionary<int, List<Vector2Int>> { };
        todayProfit = 0;
        DontDestroyOnLoad(this.gameObject);
    }
    public static string selectedSuspectName = "";
    public static float timeUntilSceneEnds = 45f;

    public static void AddMoney(int money)
    {
        totalMoney += money;
        todayProfit += money;
        UpdateMoneyText();
    }

    public static void SubMoney(int money)
    {
        totalMoney -= money;
        UpdateMoneyText();
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

    private static void ResetObjective()
    {
        todayProfit = 0;
        todayTalked = 0;
        todayServed = 0;
        isObjectiveCompleted = new bool[] { false, false, false };
    }

    public static void ResetData()
    {
        totalMoney = 20;
        UpdateMoneyText();
        suspectList = new HashSet<string>();
        todayServed = 0;
        caseNumber = 0;
        startTalking = false;
        ResetDay();
        ResetObjective();
    }

    public static void ResetToFirstNightIfLose()
    {
        totalMoney = 20;
        UpdateMoneyText();
        suspectList = new HashSet<string>();
        startTalking = false;
        ResetDay();
        ResetObjective();
    }

    public static void ResetToFirstNightIfWin()
    {
        suspectList = new HashSet<string>();
        startTalking = false;
        ResetDay();
        ResetObjective();
    }
    public static bool CheckSuspect()
    {
        return suspects[caseNumber] == selectedSuspectName;
    }
    public static bool IsLastDay()
    {
        return day == totalDays[caseNumber] - 1;
    }
    public static void NextDay()
    {
        day += 1;
        ResetObjective();
        SceneManager.LoadScene("NightScene");
    }
    public static void NextCase()
    {
        caseNumber += 1;
    }
    public static void ResetDay()
    {
        day = 0;
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
    private static void UpdateMoneyText()
    {
        moneyText = "$ " + totalMoney;
    }
}
