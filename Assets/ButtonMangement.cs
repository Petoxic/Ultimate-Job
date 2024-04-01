using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMangement : MonoBehaviour
{
    public GameObject scrollViewPanel;
    public GameObject buttonTemplate;
    public GameObject confirmationPanel;
    public GameObject stillNotSureButton;

    // Start is called before the first frame update
    void Start()
    {
        stillNotSureButton.SetActive(true);
        foreach (string name in DataManager.suspectList)
        {
            GameObject btn = (GameObject)Instantiate(buttonTemplate);
            btn.transform.SetParent(scrollViewPanel.transform);
            btn.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = name;
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
        confirmationPanel.SetActive(true);
        stillNotSureButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
