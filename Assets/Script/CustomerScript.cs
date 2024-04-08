using System;
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
    public Text objectName;
    [SerializeField] private GameObject talkBubble;
    [SerializeField] private GameObject orderBubble;
    private int dialogueIndex = 0;
    public float wordSpeed = 0.1f;
    public bool isTalking = false;
    public bool isFinishDialogue = false;
    public bool isOrderReceived = false;
    private bool isInteractable = false;
    public bool isFoodReceived = false;
    private PlayerScript player;
    private int foodId;
    public int foodAmount;
    public int dialogueChoice = 0;
    private string[][] dialogueArray = (string[][])CustomerData.customerData[1]["dialogue"];

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        dialoguePanel = canvas.transform.Find("DialogPanel").gameObject;
        dialogueText = dialoguePanel.transform.Find("Dialog").gameObject.GetComponent<Text>();
        objectName = dialoguePanel.transform.Find("Name").gameObject.GetComponent<Text>();

       
        int queue = CustomerData.getCustomerQueue();
        gameObject.name = (string)CustomerData.customerData[queue]["name"];
        dialogueArray = (string[][])CustomerData.customerData[queue]["dialogue"];
        dialogue = dialogueArray[dialogueChoice];
        foodAmount = (int)CustomerData.customerData[queue]["foodAmount"];
    }

    public void OrderDelay()
    {
        orderBubble.SetActive(true);
        isInteractable = true;
    }

    private void WaitDelay()
    {
        talkBubble.gameObject.SetActive(true);
    }

    private void EatDelay()
    {
        talkBubble.gameObject.SetActive(true);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[dialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void NextLine()
    {
        if (dialogueIndex < dialogue.Length - 1)
        {
            dialogueIndex++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            dialogueIndex = 0;
            DataManager.AddSuspectList(gameObject.name);
            ResetText();
        }
    }

    private void ResetText()
    {
        dialogueText.text = "";
        dialogueIndex = 0;
        dialoguePanel.SetActive(false);
        isTalking = false;
        DataManager.startTalking = false;
    }

    private void Update()
    {
        if (dialogueText.text == dialogue[dialogueIndex])
        {
            isTalking = false;
        }
    }

    public void OnInteract()
    {
        if (isInteractable)
        {
            if (!isOrderReceived)
            {
                isOrderReceived = true;
                orderBubble.SetActive(false);
                WaitDelay();
                player.AddOrder(foodId);
            }
            else if (!isFoodReceived && player.isHoldingFood)
            {
                if (player.holdingFoodId == foodId)
                {
                    isFoodReceived = true;
                    player.isHoldingFood = false;
                    talkBubble.gameObject.SetActive(false);
                    player.ServeOrder(foodId);
                    DataManager.AddMoney(10);
                    DataManager.AddPlateServed();
                    EatDelay();
                }
            }
            else
            {
                if (!isTalking)
                {
                    isTalking = true;
                    InteractAction();
                }
            }
        }
    }

    private void InteractAction()
    {
        objectName.text = gameObject.name;
        if (dialoguePanel.activeInHierarchy)
        {
            NextLine();
        }
        else
        {
            DataManager.startTalking = true;
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    public void setFoodId(int id)
    {
        foodId = id;
    }

    public void updateDialogueChoice()
    {
        dialogue = dialogueArray[dialogueChoice];
    }
}
