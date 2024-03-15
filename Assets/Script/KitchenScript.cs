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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (orderList.Count > 0 && !timerSlider.gameObject.activeSelf)
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
}
