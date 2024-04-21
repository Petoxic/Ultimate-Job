using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] public GameObject[] cutsceneCase;
    private GameObject[] cutsceneList;
    private GameObject[] dialogueObject;
    private int cutsceneIndex = 1;
    private TextMeshProUGUI dialogueText;
    private string dialogueString;
    private float typingSpeed = 0.06f;
    private bool canContinueToNextCutscene;
    private bool canSkip;
    private bool submitSkip;

    void Start()
    {   
        cutsceneCase[DataManager.caseNumber + 1].SetActive(true);   
        cutsceneList = GameObject.FindGameObjectsWithTag("Cutscene");
        dialogueObject = GameObject.FindGameObjectsWithTag("CutsceneText");
        canContinueToNextCutscene = true;
        canSkip = false;
        submitSkip = true;
        Array.Sort(cutsceneList, (a, b) => a.name.CompareTo(b.name));
        Array.Sort(dialogueObject, (a, b) => a.name.CompareTo(b.name));
        foreach (var cutscene in cutsceneList)
        {
            cutscene.SetActive(false);
        }

        if (cutsceneList.Length != 0)
        {
            cutsceneList[0].SetActive(true);
            dialogueText = dialogueObject[0].GetComponent<TextMeshProUGUI>();
            StartCoroutine(Typing(dialogueText.text));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            submitSkip = true;
        }

        if (canContinueToNextCutscene && Input.GetKeyDown(KeyCode.Space))
        {
            clickHandler();
        }
    }

    void clickHandler()
    {
        if (cutsceneIndex == cutsceneList.Length)
        {
            SceneManager.LoadScene("NightScene");
        }
        else if (cutsceneIndex != 0)
        {
            cutsceneList[cutsceneIndex - 1].SetActive(false);
        }
        dialogueText = dialogueObject[cutsceneIndex].GetComponent<TextMeshProUGUI>();
        StartCoroutine(Typing(dialogueText.text));
        cutsceneList[cutsceneIndex].SetActive(true);
        cutsceneIndex += 1;
    }

    IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        canContinueToNextCutscene = false;
        submitSkip = false;
        StartCoroutine(CanSkip());
        foreach (char letter in line.ToCharArray())
        {
            if (submitSkip && canSkip)
            {
                submitSkip = false;
                typingSpeed = 0.001f;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingSpeed = 0.06f;
        canContinueToNextCutscene = true;
        canSkip = false;
    }

    IEnumerator CanSkip()
    {
        canSkip = false;
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }
}
