using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinScript : MonoBehaviour
{
    private PlayerScript player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    public void OnInteract() {
        if(player.isHoldingFood) {
            DataManager.AddMoney(-5);
            player.ServeOrder(player.holdingFoodId);
            player.isHoldingFood = false;
        }
    }
}
