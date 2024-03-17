using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    private PlayerScript player;
    private List<int> orderList = new List<int>();
    private static int servingCountMax = 2;
    private List<bool> isServingSlotAvailable = Enumerable.Repeat(true, servingCountMax).ToList();
    private int servingCount = 0;
    [SerializeField] private GameObject foodPrefab;
    private Sprite[] sprites;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        sprites = Resources.LoadAll<Sprite>("food-OCAL");
    }

    void Update()
    {
        if (orderList.Count > 0 && !timerSlider.gameObject.activeSelf && !IsServingSlotFull())
        {
            timerSlider.gameObject.GetComponent<KitchenTimerScript>().currentFoodId = orderList[0];
            timerSlider.gameObject.SetActive(true);
            orderList.RemoveAt(0);
        }
    }

    public void OnInteract()
    {
        orderList.AddRange(player.orderList);
        player.ClearOrderList();
    }

    public bool IsServingSlotFull()
    {
        return servingCount == servingCountMax;
    }

    // todo: link with ing
    public void ServeOrder(int foodId)
    {
        for (int i = 0; i < servingCountMax; i++)
        {
            if (isServingSlotAvailable[i] == true)
            {
                isServingSlotAvailable[i] = false;
                servingCount += 1;
                if (i == 0)
                {
                    GameObject food = Instantiate(foodPrefab, new Vector3((float)1.205, (float)0.08, 0), Quaternion.identity);
                    food.GetComponent<FoodScript>().spriteRenderer.sprite = sprites[foodId];
                }
                else
                {
                    GameObject food = Instantiate(foodPrefab, new Vector3((float)1.205, (float)0.254, 0), Quaternion.identity);
                    food.GetComponent<FoodScript>().spriteRenderer.sprite = sprites[foodId];
                }
                return;
            }
        }
    }
}
