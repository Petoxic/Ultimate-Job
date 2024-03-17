using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    private PlayerScript player;
    private List<string> orderList = new List<string>();
    // todo: list of prefab object (change obj type in servingSlot)
    private static int servingCountMax = 2;
    private List<int> servingSlot = new List<int>(servingCountMax);
    private int servingCount = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        // todo: check if servingSlot is not fully occupied
        if (orderList.Count > 0 && !timerSlider.gameObject.activeSelf && !IsServingSlotFull())
        {
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
    public void ServeOrder()
    {
        foreach (int passing in servingSlot)
        {
            // if serving is empty then setActive with specified image then servingCount += 1
        }
    }
}
