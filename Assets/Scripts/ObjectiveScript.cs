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
        objective_1 = GameObject.Find("ObjectiveText_1").GetComponent<Text>();
        objective_2 = GameObject.Find("ObjectiveText_2").GetComponent<Text>();
        objective_3 = GameObject.Find("ObjectiveText_3").GetComponent<Text>();

        objective_1.text = Utils.GetProfitObjText();
        objective_2.text = Utils.GetTalkedObjText();
        objective_3.text = Utils.GetServedObjText();
    }

    void Update()
    {
        // TODO: call this function only when the objective is updated
        objective_1.text = Utils.GetProfitObjText();
        objective_2.text = Utils.GetTalkedObjText();
        objective_3.text = Utils.GetServedObjText();
    }
}
