using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCount : MonoBehaviour
{
    public GameObject DayText;
    // Start is called before the first frame update
    void Start()
    {
        DayText.GetComponent<UnityEngine.UI.Text>().text = "Case " + DataManager.caseNumber + " Day " + DataManager.day;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
