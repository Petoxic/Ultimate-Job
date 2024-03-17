using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static int totalMoney = 0;
    public static string moneyText = "Total money: 0";
    public static List<string> peopleList;
    public static List<bool> isObjectiveCompleted;

    public static void AddMoney(int money)
    {
        totalMoney += money;
        moneyText = "Total money: " + totalMoney;
    }
}
