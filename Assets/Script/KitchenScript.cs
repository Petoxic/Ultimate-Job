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
    private static readonly int ordersCountMax = 2;
    private readonly List<int> orderBuffer = new();
    private readonly List<int> orderSlots = Enumerable.Repeat(-1, ordersCountMax).ToList();
    private const float orderSlotX = 1.52f;
    private const float firstOrderSlotY = 0.4f;
    private const float orderSlotGap = 0.174f;
    [SerializeField] private GameObject foodPrefab;
    private Sprite[] sprites;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        sprites = Resources.LoadAll<Sprite>("food-OCAL");
    }

    void Update()
    {
        if (orderBuffer.Count > 0 && !timerSlider.gameObject.activeSelf && !IsServingSlotFull())
        {
            timerSlider.gameObject.GetComponent<KitchenTimerScript>().currentFoodId = orderBuffer[0];
            timerSlider.gameObject.SetActive(true);
            orderBuffer.RemoveAt(0);
        }
    }

    public void OnInteract()
    {
        orderBuffer.AddRange(player.orderList);
        player.ClearOrderList();
    }

    public int ReadyOrdersCount()
    {
        int count = 0;
        foreach (int order in orderSlots)
        {
            if (order != -1)
            {
                count += 1;
            }
        }

        return count;
    }

    public bool IsServingSlotFull()
    {
        Debug.Log("serving count " + ReadyOrdersCount());
        return ReadyOrdersCount() == ordersCountMax;
    }

    public void FinishOrder(int foodId)
    {
        for (int i = 0; i < ordersCountMax; i++)
        {
            if (orderSlots[i] == -1)
            {
                GameObject food = Instantiate(foodPrefab, new Vector3(orderSlotX, firstOrderSlotY + i * orderSlotGap, 0), Quaternion.identity);
                FoodScript foodScript = food.GetComponent<FoodScript>();
                foodScript.spriteRenderer.sprite = sprites[foodId];
                foodScript.spriteRenderer.sortingOrder = 5;
                foodScript.foodId = foodId;
                foodScript.foodInstanceId = food.GetInstanceID();

                Debug.Log("adding food instance id " + food.GetInstanceID());
                orderSlots[i] = food.GetInstanceID();

                return;
            }
        }
    }

    public void PickUpOneFood(int foodInstanceId)
    {
        Debug.Log("removing food instance id " + foodInstanceId);
        for (int i = 0; i < ordersCountMax; i++)
        {
            if (orderSlots[i] == foodInstanceId)
            {
                orderSlots[i] = -1;
                return;
            }
        }
    }
}
