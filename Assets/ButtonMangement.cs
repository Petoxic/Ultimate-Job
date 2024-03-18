using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMangement : MonoBehaviour
{
    public GameObject scrollViewPanel;
    public GameObject buttonTemplate;
    public GameObject confirmationPanel;
    public GameObject stillNotSureButton;

    List<string> names = new List<string>() { "George", "Isora", "J3cha" };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < names.Count; i++)
        {
            GameObject btn = (GameObject)Instantiate(buttonTemplate);
            btn.transform.SetParent(scrollViewPanel.transform);
            btn.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = names[i];
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
        stillNotSureButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
