using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private GameObject[] tutorialList;
    private int tutorialIndex = 0;
    [SerializeField] private GameObject keyText;
    [SerializeField] private GameObject welcomeBanner;

    void clickHandler()
    {
        if (tutorialIndex != 0)
        {
            tutorialList[tutorialIndex - 1].SetActive(false);
        }

        if (tutorialIndex == tutorialList.Count())
        {
            // show text
            keyText.SetActive(true);
        }
        else
        {
            keyText.SetActive(false);
        }

        tutorialList[tutorialIndex].SetActive(true);
        tutorialIndex += 1;
    }
    void Start()
    {
        tutorialList = GameObject.FindGameObjectsWithTag("TutorialText");
        Array.Sort(tutorialList, (a, b) => a.name.CompareTo(b.name));
        Debug.Log(tutorialList.Count());
        foreach (var tutorial in tutorialList)
        {
            // Debug.Log(tutorial.transform.Find("Text").gameObject.GetComponent<Text>().text);
            tutorial.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            welcomeBanner.SetActive(false);
            clickHandler();
        }
        else if (Input.GetKeyDown(KeyCode.R) && keyText.activeSelf == true)
        {
            tutorialIndex = 0;
            clickHandler();
        }
        else if (Input.GetKeyDown(KeyCode.E) && keyText.activeSelf == true)
        {
            SceneManager.LoadScene("PrologueCutscene");
        }
    }
}
