using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    private float timeWhenSceneEnds;

    void Start()
    {
        timeWhenSceneEnds = DataManager.timeUntilSceneEnds;
    }

    void Update()
    {
        if (!DataManager.startTalking)
        {
            timeWhenSceneEnds -= Time.deltaTime;
            if (timeWhenSceneEnds <= 0)
            {
                DataManager.isGameEnd = true;
                SceneManager.LoadScene("ResultScene");
            }
        }
    }
}
