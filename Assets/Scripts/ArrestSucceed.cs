using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrestSucceed : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject gameWin;
    public GameObject confirmationPanel;
    public GameObject stillNotSureButton;

    void Start()
    {
        gameOver.SetActive(false);
        gameWin.SetActive(false);
        if (DataManager.IsLastDay())
        {
            stillNotSureButton.SetActive(false);
        }
        else
        {
            stillNotSureButton.SetActive(true);
        }
    }

    public void YesClicked()
    {
        if (DataManager.CheckSuspect())
        {
            gameWin.SetActive(true);
            gameOver.SetActive(false);
        }
        else
        {
            gameWin.SetActive(false);
            gameOver.SetActive(true);
        }
    }

    public void NoClicked()
    {
        confirmationPanel.SetActive(false);
    }
}
