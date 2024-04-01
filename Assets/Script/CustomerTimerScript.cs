using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerTimerScript : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private float countdownTime = 20f;
    private CustomerScript customerScript;

    void OnEnable()
    {
        timerSlider.maxValue = countdownTime;
        timerSlider.value = countdownTime;

        customerScript = customer.GetComponent<CustomerScript>();
    }

    void Update()
    {
        if (timerSlider.gameObject.activeSelf && !DataManager.startTalking)
        {
            Debug.Log("reducing");
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
                    customerScript.updateDialogueChoice();
                    customerScript.OrderDelay();
                    gameObject.SetActive(false);
                    customerScript.isOrderReceived = false;
                    customerScript.isFoodReceived = false;
                }
            }
        }
    }
}
