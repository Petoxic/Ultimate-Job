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
    public void OnInteract()
    {
        if (player.isHoldingFood)
        {
            DataManager.SubMoney(5);
            player.ServeOrder(player.holdingFoodId, "-5");
            player.isHoldingFood = false;
        }
    }
}
