using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrestSucceed : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject gameWin;
    public GameObject confirmationPanel;

    void Start()
    {
        gameOver.SetActive(false);
        gameWin.SetActive(false);
    }

    public void YesClicked()
    {
        if (DataManager.selectedSuspectName == "Namo")
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
