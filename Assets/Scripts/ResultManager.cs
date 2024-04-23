using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;

public class ResultManager : MonoBehaviour
{
    public TMP_Text header;
    public Text colorchangingFont1;
    public Text colorchangingFont2;
    public Text colorchangingFont3;
    public Image star1;
    public Image star2;
    public Image star3;

    public GameObject foundCriminalButton;
    public GameObject notSureButton;

    private Sprite[] sprites;

    // Start is called before the first frame update
    void OnEnable()
    {
        sprites = Resources.LoadAll<Sprite>("star");
        if (DataManager.isObjectiveCompleted[0] && DataManager.isObjectiveCompleted[1] && DataManager.isObjectiveCompleted[2])
        {
            notSureButton.SetActive(false);
            foundCriminalButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Criminal List";

            header.text = "Objective Completed!";
            header.color = Color.yellow;

            foundCriminalButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                GoToArrestingScene();
            });

            notSureButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                DataManager.NextDay();
            });

        }
        else
        {
            header.text = "Objective Failed!";
            header.color = Color.red;
            foundCriminalButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Restart";
            notSureButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Back to main menu";

            foundCriminalButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                DataManager.ResetCase();
            });

            notSureButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                DataManager.RestartGame();
                GoToMainMenu();
            });

        }
        colorchangingFont1.text = Utils.GetProfitObjText();
        colorchangingFont2.text = Utils.GetTalkedObjText();
        colorchangingFont3.text = Utils.GetServedObjText();
        if (DataManager.isObjectiveCompleted[0])
        {
            colorchangingFont1.color = Color.yellow;
            star1.sprite = sprites[sprites.Length - 1];
        }
        else
        {
            colorchangingFont1.color = Color.red;
            star1.sprite = sprites[sprites.Length - 2];
        }
        if (DataManager.isObjectiveCompleted[1])
        {
            colorchangingFont2.color = Color.yellow;
            star2.sprite = sprites[sprites.Length - 1];
        }
        else
        {
            colorchangingFont2.color = Color.red;
            star2.sprite = sprites[sprites.Length - 2];
        }
        if (DataManager.isObjectiveCompleted[2])
        {
            colorchangingFont3.color = Color.yellow;
            star3.sprite = sprites[sprites.Length - 1];
        }
        else
        {
            colorchangingFont3.color = Color.red;
            star3.sprite = sprites[sprites.Length - 2];
        }

        if (DataManager.caseNumber == 0)
        {
            GameObject.FindGameObjectWithTag("Map1").SetActive(true);
            GameObject.FindGameObjectWithTag("Map2").SetActive(false);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Map2").SetActive(true);
            GameObject.FindGameObjectWithTag("Map1").SetActive(false);
        }
    }

    public void GoToArrestingScene()
    {
        SceneManager.LoadScene("ArrestingScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
