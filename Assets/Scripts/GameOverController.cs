using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public static void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}