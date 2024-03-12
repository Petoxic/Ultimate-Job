using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index = 0;
    public float wordSpeed = 0.1f;
    public bool isTalking = false;
    public bool isCollide = false;
    public bool isFinishDialogue = false;

    IEnumerator Typing() {
        foreach(char letter in dialogue[index].ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void NextLine() {
        if (index < dialogue.Length - 1) {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        } else {
            ResetText();
        }
    }

    private void ResetText() {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && isCollide) {
            OnInteract();
        }
        if (dialogueText.text == dialogue[index]) {
            isTalking = false;
        }
    }

    private void OnInteract()
    {
        if (!isTalking) {
            isTalking = true;
            InteractAction();
        }
    }

    private void InteractAction()
    {
        // Debug.Log(name + " is talking");
        if (dialoguePanel.activeInHierarchy) {
            NextLine();
        } else {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    private void OnCollisionEnter2D() {
        isCollide = true;
    }

    private void OnCollisionExit2D() {
        isCollide = false;
    }
}