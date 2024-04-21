using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCount : MonoBehaviour
{
    public GameObject DayText;
    // Start is called before the first frame update
    void Start()
    {
        DayText.GetComponent<UnityEngine.UI.Text>().text = $"Day {DataManager.GetDay() + 1}/3, Case {DataManager.GetCaseNumber() + 1}";
    }
}
