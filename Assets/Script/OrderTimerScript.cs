using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderTimerScript : MonoBehaviour
{
    [SerializeField] private GameObject orderBubble;
    [SerializeField] private Slider timerSlider;
    private float countdownTime = 10f;
    [SerializeField] private Image foodImage;
    private Sprite[] sprites;
    private CustomerScript customerScript;

    void OnEnable()
    {
        timerSlider.maxValue = countdownTime;
        timerSlider.value = countdownTime;
    }

    void Start()
    {
        customerScript = GetComponentInParent<CustomerScript>();
        sprites = Resources.LoadAll<Sprite>("food-OCAL");

        int foodId = UnityEngine.Random.Range(40, 57);
        foodImage.sprite = sprites[foodId];
        customerScript.setFoodId(foodId);
    }

    void Update()
    {
        if (timerSlider.gameObject.activeSelf)
        {
            timerSlider.value -= Time.deltaTime;
            if (timerSlider.value == 0)
            {
                orderBubble.SetActive(false);
            }
        }
    }
}
