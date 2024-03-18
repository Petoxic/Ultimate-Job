using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveScript : MonoBehaviour
{
    private Text objective_1;
    private Text objective_2;
    private Text objective_3;

    void Start()
    {
        objective_1 = GameObject.Find("ObjectiveText_1").gameObject.GetComponent<Text>();
        objective_2 = GameObject.Find("ObjectiveText_2").gameObject.GetComponent<Text>();
        objective_3 = GameObject.Find("ObjectiveText_3").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        objective_1.text = "Money: " + DataManager.totalMoney + "/20";
        objective_2.text = "People talked: " + DataManager.suspectList.Count + "/2";
        objective_3.text = "Dish served: " + DataManager.plateServed + "/2";
    }
}
