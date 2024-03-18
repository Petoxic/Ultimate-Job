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
        stillNotSureButton.SetActive(false);
    }

    public void YesClicked()
    {
        if (DataManager.selectedSuspectName == "J3cha")
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
        stillNotSureButton.SetActive(true);
        confirmationPanel.SetActive(false);
    }
}
