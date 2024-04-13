using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayButtonController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // SceneManager.LoadScene("MainMenu");
            Debug.Log("Main Menu");
        }
    }
}
