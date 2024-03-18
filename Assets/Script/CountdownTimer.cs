using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    private float timeWhenSceneEnds;

    void Start()
    {
        timeWhenSceneEnds = Time.time + DataManager.timeUntilSceneEnds;
    }

    void Update()
    {
        if (timeWhenSceneEnds <= Time.time)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
