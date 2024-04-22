using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTimerScript : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    private CustomerScript customerScript;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private Image iconImage;
    public bool isWaiting = true;
    void OnEnable()
    {
        customerScript = customer.GetComponent<CustomerScript>();

        if (isWaiting)
        {
            timerSlider.maxValue = customerScript.waitingForFoodTime;
        }
        else
        {
            timerSlider.maxValue = customerScript.eatingTime;
        }
        timerSlider.value = customerScript.waitingForFoodTime;
    }

    void Update()
    {
        if (timerSlider.gameObject.activeSelf && !DataManager.startTalking)
        {
            timerSlider.value -= Time.deltaTime;
            if (timerSlider.value == 0)
            {
                if (customerScript.foodAmount == 1)
                {
                    customer.SetActive(false);
                }
                else
                {
                    customerScript.foodAmount -= 1;
                    customerScript.dialogueChoice += 1;
                    customerScript.UpdateDialogueChoice();
                    customerScript.OrderDelay();
                    gameObject.SetActive(false);
                    customerScript.isOrderReceived = false;
                    customerScript.isFoodReceived = false;
                }
            }
        }
    }

    public void SetIconImage(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}
