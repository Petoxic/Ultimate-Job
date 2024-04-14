using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArrestController : MonoBehaviour
{
    public GameObject scrollViewPanel;
    public GameObject buttonTemplate;
    public GameObject confirmationPanel;
    public Button stillNotSureButton;

    // Start is called before the first frame update
    void Start()
    {
        stillNotSureButton.onClick.AddListener(delegate
        {
            StillNotSure();
        });

        foreach (string name in DataManager.suspectList)
        {
            GameObject btn = (GameObject)Instantiate(buttonTemplate);
            btn.transform.SetParent(scrollViewPanel.transform);
            btn.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = name;
            btn.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Custoner/" + name);
            btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                DataManager.selectedSuspectName = btn.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
                GoToConfirmation();
            });
            btn.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public void GoToConfirmation()
    {
        Debug.Log(DataManager.selectedSuspectName);
        confirmationPanel.SetActive(true);
    }

    public void StillNotSure()
    {
        if (DataManager.isGameEnd)
        {
            // Go to next day
            DataManager.day += 1;
            DataManager.ResetObjective();
            SceneManager.LoadScene("NightScene");
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
}
