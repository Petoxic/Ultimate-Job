using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenTimerScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    [SerializeField] private float countdownTime = 10f;

    void OnEnable()
    {
        timerSlider.maxValue = countdownTime;
        timerSlider.value = countdownTime;
    }

    void Update()
    {
        if (timerSlider.gameObject.activeSelf)
        {
            timerSlider.value -= Time.deltaTime;
            if (timerSlider.value == 0)
            {
                timerSlider.gameObject.SetActive(false);
            }
        }
    }
}
