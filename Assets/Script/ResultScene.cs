using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultMenu : MonoBehaviour
{
    public void ToArrestingScene()
    {
        SceneManager.LoadScene("ArrestingScene");
    }
}
