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
        DataManager.AddMoney(-5);
        player.ServeOrder(player.holdingFoodId);
    }
}
