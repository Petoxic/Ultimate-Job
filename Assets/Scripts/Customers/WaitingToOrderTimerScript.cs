using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingToOrderTimerScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    [SerializeField] private Image foodImage;
    private CustomerScript customerScript;
    [SerializeField] private GameObject customer;

    void OnEnable()
    {
        customerScript = GetComponentInParent<CustomerScript>();
        timerSlider.maxValue = customerScript.waitingToOrderTime;
        timerSlider.value = customerScript.waitingToOrderTime;

        foodImage.sprite = customerScript.GetFoodSprite();
    }

    void Update()
    {
        if (timerSlider.gameObject.activeSelf && !DataManager.startTalking)
        {
            timerSlider.value -= Time.deltaTime;
            if (timerSlider.value == 0)
            {
                customer.SetActive(false);
            }
        }
    }
}
