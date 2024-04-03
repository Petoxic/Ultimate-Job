using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    // public GameObject stillNotSureButton;

    // void Start()
    // {
    //     stillNotSureButton.SetActive(false);
    // }

    // ToNightScene?
    public void ToRestart()
    {
        // Clear Data in DataManagement

        // Same Case, Night 1
        DataManager.ResetToFirstNightIfLose();
        SceneManager.LoadScene("NightScene");

    }

    public void ToNextMission()
    {
        // Next Case, Night 1
        DataManager.caseNumber += 1;
        DataManager.ResetToFirstNightIfWin();
        SceneManager.LoadScene("NightScene");
    }

    public void StillNotSure()
    {
        // Same Case, Next Night
        DataManager.ResetObjective();
        DataManager.day += 1;
        SceneManager.LoadScene("NightScene");
    }
}
