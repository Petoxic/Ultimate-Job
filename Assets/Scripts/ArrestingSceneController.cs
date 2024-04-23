using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArrestingSceneController : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewPanel;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private Button stillNotSureButton;
    [SerializeField] private GameObject gameLostModal;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private GameObject gameWonModal;
    [SerializeField] private Button nextCaseButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    private Sprite[] sprites;
    [SerializeField] private Text gameWinText;
    [SerializeField] private Text rewardText;
    [SerializeField] private Text conclusionText;

    // Start is called before the first frame update
    void Start()
    {
        if (stillNotSureButton != null)
        {
            stillNotSureButton.onClick.AddListener(delegate
            {
                StillNotSure();
            });
        }
        if (restartGameButton != null)
        {
            restartGameButton.onClick.AddListener(delegate
            {
                DataManager.ResetCase();
            });
        }
        if (nextCaseButton != null)
        {
            nextCaseButton.onClick.AddListener(delegate
            {
                DataManager.SetPreviousCaseStartingMoney(DataManager.GetTotalMoney());
                DataManager.NextCase();
            });
        }
        yesButton.onClick.AddListener(delegate
        {
            YesClicked();
        });
        noButton.onClick.AddListener(delegate
        {
            NoClicked();
        });

        foreach (string name in DataManager.suspectList)
        {
            GameObject btn = (GameObject)Instantiate(buttonTemplate);
            btn.transform.SetParent(scrollViewPanel.transform);
            btn.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = name;
            sprites = Resources.LoadAll<Sprite>("Custoner/" + name);
            btn.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = sprites[1];
            btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                DataManager.selectedSuspectName = btn.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
                GoToConfirmation();
            });
            btn.transform.localScale = new Vector3(1, 1, 1);
        }

        if (DataManager.GetDay() == 2)
        {
            stillNotSureButton.gameObject.SetActive(false);
        }//

    }

    void Awake()
    {
        if (DataManager.IsLastCase())
        {
            gameWinText.text = "Go to final scene";
        }
    }

    private void GoToConfirmation()
    {
        Debug.Log(DataManager.selectedSuspectName);
        confirmationPanel.SetActive(true);
    }

    private void StillNotSure()
    {
        if (DataManager.IsDayEnded())
        {
            // Go to next day
            DataManager.NextDay();
        }
        else
        {
            // Resume the game
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync("ArrestingScene");
            if (asyncOperation == null)
            {
                Debug.LogError("Failed to unload scene");
            }
        }
    }

    private void YesClicked()
    {
        if (DataManager.CheckSuspect())
        {
            Debug.Log("Correct");
            string reward = "";
            if (DataManager.GetDay() + 1 == 1)
            {
                DataManager.Reward(120);
                reward = "120";
            }
            else if (DataManager.GetDay() + 1 == 2)
            {
                DataManager.Reward(70);
                reward = "70";
            }
            else if (DataManager.GetDay() + 1 == 3)
            {
                DataManager.Reward(20);
                reward = "20";
            }
            rewardText.text = "Reward +$" + reward;
            conclusionText.text = Utils.GetConclusionText();
            gameWonModal.SetActive(true);
            gameLostModal.SetActive(false);
        }
        else
        {
            gameWonModal.SetActive(false);
            gameLostModal.SetActive(true);
        }
    }

    private void NoClicked()
    {
        confirmationPanel.SetActive(false);
    }
}
