using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalMoneyTextScript : MonoBehaviour
{
    [SerializeField] private Text text;
    void Start()
    {
        text.text = DataManager.moneyText;
    }
}
