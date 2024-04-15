using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalMoneyTextScript : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = DataManager.GetMoneyText();
    }

    void Update()
    {
        text.text = DataManager.GetMoneyText();
    }
}
