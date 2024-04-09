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
        objective_1.text = "Profit: " + DataManager.todayMoney + "/20"; //today money
        objective_2.text = "People talked: " + DataManager.todayTalked + "/2"; //today talked
        objective_3.text = "Dish served: " + DataManager.plateServed + "/2"; //today served
    }

    void Update()
    {
        objective_1.text = "Profit: " + DataManager.todayMoney + "/20"; //today money
        objective_2.text = "People talked: " + DataManager.todayTalked + "/2"; //today talked
        objective_3.text = "Dish served: " + DataManager.plateServed + "/2"; //today served
    }
}
