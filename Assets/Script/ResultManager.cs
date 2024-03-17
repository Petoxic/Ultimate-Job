using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public ResultScreen ResultScreen;

    // Init all parameters showed as result
    int totalIncome = 0;
    int[] objectives = { 0 };
    string[] suspectList = { "John" };

    public void GameResult()
    {
        ResultScreen.Setup(objectives);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
