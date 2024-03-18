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
    [SerializeField] private Slider timerSlider;
    [SerializeField] private GameObject orderBubble;
    private int dialogueIndex = 0;
    public float wordSpeed = 0.1f;
    public bool isTalking = false;
    public bool isFinishDialogue = false;
    private bool isOrderReceived = false;
    private bool isInteractable = false;
    private bool isFoodReceived = false;
    private PlayerScript player;
    private int foodId;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(OrderDelay());
    }

    private IEnumerator OrderDelay()
    {
        yield return new WaitForSeconds(3);
        orderBubble.gameObject.SetActive(true);
        isInteractable = true;
    }

    private IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(5);
        timerSlider.gameObject.SetActive(true);
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
                DataManager.AddPlateServed();
                orderBubble.gameObject.SetActive(false);
                StartCoroutine(WaitDelay());
                player.AddOrder(foodId);
            }
            else if (!isFoodReceived && player.isHoldingFood)
            {
                if (player.holdingFoodId == foodId)
                {
                    isFoodReceived = true;
                    player.isHoldingFood = false;
                    timerSlider.gameObject.SetActive(false);
                    player.ServeOrder(foodId);
                    DataManager.AddMoney(10);
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
        // todo: fix this bug: name not show on dialog name
        // objectName.text = gameObject.name;
        if (dialoguePanel.activeInHierarchy)
        {
            NextLine();
        }
        else
        {
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    public void setFoodId(int id)
    {
        foodId = id;
    }
}
