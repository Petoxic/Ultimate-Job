using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenTimerScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    [SerializeField] private float countdownTime = 2f;
    private KitchenScript kitchen;
    public int currentFoodId = 0;

    void OnEnable()
    {
        Debug.Log("OnEnable " + currentFoodId);
        timerSlider.maxValue = countdownTime - (DataManager.cookingLevels * 0.1f);
        timerSlider.value = countdownTime -  (DataManager.cookingLevels * 0.1f);
    }

    void Start()
    {
        kitchen = GameObject.FindGameObjectWithTag("Kitchen").GetComponent<KitchenScript>();
    }

    void Update()
    {
        if (timerSlider.gameObject.activeSelf && !DataManager.startTalking)
        {
            timerSlider.value -= Time.deltaTime;
            if (timerSlider.value == 0)
            {
                timerSlider.gameObject.SetActive(false);
                kitchen.FinishOrder(currentFoodId);
            }
        }
    }
}
