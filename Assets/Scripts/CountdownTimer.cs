using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Image timer_progress_bar;
    private float timeWhenSceneEnds;
    private float max_time;

    void Start()
    {
        timeWhenSceneEnds = DataManager.timeUntilSceneEnds;
        max_time = DataManager.timeUntilSceneEnds;
    }

    void Update()
    {
        if (!DataManager.startTalking)
        {
            timeWhenSceneEnds -= Time.deltaTime;
            timer_progress_bar.fillAmount = timeWhenSceneEnds / max_time;
            if (timeWhenSceneEnds <= 0)
            {
                DataManager.CheckObjective();
                DataManager.SetDayEnded(true);
                SceneManager.LoadScene("ResultScene");
            }
        }
    }
}
