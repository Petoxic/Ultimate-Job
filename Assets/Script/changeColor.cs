using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColor : MonoBehaviour
{
    public Text header;
    public Text colorchangingFont1;
    public Text colorchangingFont2;
    public Text colorchangingFont3;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.isObjectiveCompleted[0] && DataManager.isObjectiveCompleted[1] && DataManager.isObjectiveCompleted[2])
        {
            header.text = "Done!";
        }
        else
        {
            header.text = "Game Over!";
        }
        if (DataManager.isObjectiveCompleted[0])
        {
            colorchangingFont1.color = Color.yellow;
        }
        else
        {
            colorchangingFont1.color = Color.red;
        }
        if (DataManager.isObjectiveCompleted[1])
        {
            colorchangingFont2.color = Color.yellow;
        }
        else
        {
            colorchangingFont2.color = Color.red;
        }
        if (DataManager.isObjectiveCompleted[2])
        {
            colorchangingFont3.color = Color.yellow;
        }
        else
        {
            colorchangingFont3.color = Color.red;
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
