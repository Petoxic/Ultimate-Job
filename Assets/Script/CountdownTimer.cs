using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{

    // Init for test only
    private float timeUntilSceneEnds = 5f;
    private float timeWhenSceneEnds;

    void Start()
    {
        timeWhenSceneEnds = Time.time + timeUntilSceneEnds;
    }

    void Update()
    {
        if (timeWhenSceneEnds <= Time.time)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
