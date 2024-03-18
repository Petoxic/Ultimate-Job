using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static int totalMoney = 0;
    public static string moneyText;
    public static HashSet<string> suspectList = new HashSet<string>();
    public static List<bool> isObjectiveCompleted = new List<bool> { false, false, false };
    public static int plateServed = 0;

    void Start()
    {
        moneyText = "Total money: 0";
    }

    public static void AddMoney(int money)
    {
        totalMoney += money;
        moneyText = "Total money: " + totalMoney;
        checkObjective();
    }

    public static void AddSuspectList(string suspect)
    {
        suspectList.Add(suspect);
        checkObjective();
    }

    public static void AddPlateServed()
    {
        plateServed += 1;
        checkObjective();
    }

    public static void checkObjective()
    {
        /* 1. money >= 20
            2. talk with >= 2 people
            3. serve >= 2 people
        */
        if (totalMoney >= 20)
        {
            isObjectiveCompleted[0] = true;
        }
        if (suspectList.Count >= 2)
        {
            isObjectiveCompleted[1] = true;
        }
        if (plateServed >= 2)
        {
            isObjectiveCompleted[2] = true;
        }
    }
}
