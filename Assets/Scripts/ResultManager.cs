using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    void Start()
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
                GoToNextNightScene();
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
                GoToFirstNightScene();
            });

            notSureButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                DataManager.ResetData();
                GoToMainMenu();
            });

        }
        colorchangingFont1.text = "Profit: " + DataManager.todayMoney + "/20"; 
        colorchangingFont2.text = "People talked: " + DataManager.todayTalked + "/2"; 
        colorchangingFont3.text = "Dish served: " + DataManager.plateServed + "/2"; 
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
