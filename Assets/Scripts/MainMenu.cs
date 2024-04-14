using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void SettingButton()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}