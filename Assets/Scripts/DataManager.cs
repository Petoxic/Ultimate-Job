using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static int totalMoney;
    public static string moneyText;
    public static HashSet<string> suspectList;
    public static List<bool> isObjectiveCompleted;
    public static int plateServed;
    public static int day;
    public static int caseNumber;
    public static int todayMoney;
    public static int todayTalked;
    public static bool isGameEnd = false;
    public static bool startTalking;
    public static float cellSize;
    public static HashSet<string> appearedCharactors;
    public static Dictionary<int, List<Vector2Int>> placedObjectsData;

    void Start()
    {
        totalMoney = 20;
        moneyText = "$ 0";
        appearedCharactors = new HashSet<string>();
        suspectList = new HashSet<string>();
        isObjectiveCompleted = new List<bool> { false, false, false };
        plateServed = 0;
        day = 1;
        caseNumber = 1;
        todayTalked = 0;
        startTalking = false;
        cellSize = 0.16f;
        placedObjectsData = new Dictionary<int, List<Vector2Int>> { };
        todayMoney = 0;
    }
    public static string selectedSuspectName = "";
    public static float timeUntilSceneEnds = 45f;

    public static void AddMoney(int money)
    {
        totalMoney += money;
        todayMoney += money;
        moneyText = "$ " + totalMoney;
        checkObjective();
    }

    public static void SubMoney(int money)
    {
        totalMoney -= money;
        moneyText = "$ " + totalMoney;
    }

    public static void AddSuspectList(string suspect)
    {
        if (!suspectList.Contains(suspect))
        {
            todayTalked += 1;
        }
        suspectList.Add(suspect);
        checkObjective();
    }

    public static void AddPlateServed()
    {
        plateServed += 1;
        checkObjective();
    }
    //TODO Objective
    public static void checkObjective()
    {
        /* 1. money >= 20
            2. talk with >= 2 people
            3. serve >= 2 people
        */
        if (todayMoney >= 20)
        {
            isObjectiveCompleted[0] = true;
        }
        if (todayTalked >= 2)
        {
            isObjectiveCompleted[1] = true;
        }
        if (plateServed >= 2)
        {
            isObjectiveCompleted[2] = true;
        }
    }

    public static void ResetObjective()
    {
        todayMoney = 0;
        todayTalked = 0;
        plateServed = 0;
        isObjectiveCompleted = new List<bool> { false, false, false };
    }

    public static void ResetData()
    {
        totalMoney = 20;
        moneyText = "$ " + totalMoney;
        suspectList = new HashSet<string>();
        isObjectiveCompleted = new List<bool> { false, false, false };
        plateServed = 0;
        day = 1;
        caseNumber = 1;
        startTalking = false;
        ResetObjective();
    }

    public static void ResetToFirstNightIfLose()
    {
        totalMoney = 20;
        moneyText = "$ " + totalMoney;
        suspectList = new HashSet<string>();
        isObjectiveCompleted = new List<bool> { false, false, false };
        day = 1;
        startTalking = false;
        ResetObjective();
    }

    public static void ResetToFirstNightIfWin()
    {
        suspectList = new HashSet<string>();
        isObjectiveCompleted = new List<bool> { false, false, false };
        day = 1;
        startTalking = false;
        ResetObjective();
    }

}
