using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public Text header;
    public Text colorchangingFont1;
    public Text colorchangingFont2;
    public Text colorchangingFont3;

    public GameObject foundCriminalButton;
    public GameObject notSureButton;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.isObjectiveCompleted[0] && DataManager.isObjectiveCompleted[1] && DataManager.isObjectiveCompleted[2])
        {
            if (DataManager.day == 3)
            {
                notSureButton.SetActive(false);
                foundCriminalButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "I think I found CRIMINAL";
            }else{
                foundCriminalButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "I found CRIMINAL";
            }
            header.text = "Objective Completed!";
            
            notSureButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Not sure";

            foundCriminalButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                GoToArrestingScene();
            });

            notSureButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                
                GoToNextNightScene();
            });

        }
        else
        {
            header.text = "Objective Failed!";
            foundCriminalButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Restart";
            notSureButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Back to main menu";

            foundCriminalButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                GoToFirstNightScene();
            });

            notSureButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                DataManager.ResetData();
                GoToMainMenu();
            });

        }
        if (DataManager.isObjectiveCompleted[0])
        {
            colorchangingFont1.color = Color.yellow;
        }
        else
        {
            colorchangingFont1.color = Color.red;
        }
        if (DataManager.isObjectiveCompleted[1])
        {
            colorchangingFont2.color = Color.yellow;
        }
        else
        {
            colorchangingFont2.color = Color.red;
        }
        if (DataManager.isObjectiveCompleted[2])
        {
            colorchangingFont3.color = Color.yellow;
        }
        else
        {
            colorchangingFont3.color = Color.red;
        }



    }

    public void GoToArrestingScene()
    {
        SceneManager.LoadScene("ArrestingScene");
    }

    public void GoToNextNightScene()
    {
        DataManager.day += 1;
        DataManager.ResetObjective();
        SceneManager.LoadScene("NightScene");
    }

    public void GoToFirstNightScene()
    {
        DataManager.ResetToFirstNightIfLose();
        SceneManager.LoadScene("NightScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
