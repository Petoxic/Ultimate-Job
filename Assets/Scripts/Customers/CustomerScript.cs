using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour
{
    public readonly float waitingToOrderTime = 20f;
    public readonly float waitingForFoodTime = 20f;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    public Text objectName;
    [SerializeField] private GameObject talkBubble;
    [SerializeField] private GameObject orderBubble;
    [SerializeField] private GameObject talkingImage;
    [SerializeField] private CustomerTimerScript customerTimerScript;
    private int dialogueIndex = 0;
    public float wordSpeed = 0.1f;
    public bool isTalking = false;
    public bool isFinishDialogue = false;
    public bool isOrderReceived = false;
    private bool isInteractable = false;
    public bool isFoodReceived = false;
    private PlayerScript player;
    private Sprite[] foodSprites;
    private int foodId;
    public int foodAmount;
    private Sprite eatingSprite;
    public int dialogueChoice = 0;
    private string[][] dialogueArray = (string[][])CustomerData.customerData[1]["dialogue"];
    public SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Custoner/" + gameObject.name);

        customerTimerScript = GetComponentInChildren<CustomerTimerScript>(true);

        foodSprites = Resources.LoadAll<Sprite>("food-OCAL");
        foodId = UnityEngine.Random.Range(40, 57);

        eatingSprite = Resources.Load<Sprite>("eating-icon");
    }

    public void OrderDelay()
    {
        orderBubble.SetActive(true);
        isInteractable = true;
    }

    private void WaitDelay()
    {
        talkBubble.SetActive(true);
        customerTimerScript.SetIconImage(foodSprites[foodId]);
    }

    private void EatDelay()
    {
        talkBubble.SetActive(true);
        // change icon to eating
        customerTimerScript.SetIconImage(eatingSprite);
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
            wordSpeed = 0.1f;
            DataManager.AddSuspectList(gameObject.name);
            talkingImage.SetActive(false);
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
                talkingImage.SetActive(true);
            }
            else if (!isFoodReceived && player.isHoldingFood)
            {
                if (player.holdingFoodId == foodId)
                {
                    isFoodReceived = true;
                    player.isHoldingFood = false;
                    talkBubble.SetActive(false);
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
                else
                {
                    wordSpeed = 0.00000001f;
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
    public Sprite GetFoodSprite()
    {
        return foodSprites[foodId];
    }
    public void UpdateDialogueChoice()
    {
        dialogue = dialogueArray[dialogueChoice];
    }
}