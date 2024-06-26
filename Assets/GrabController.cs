using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform foodHolder;
    public float rayDist;
    private PlayerScript player;
    private KitchenScript kitchenScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        kitchenScript = GameObject.FindGameObjectWithTag("Kitchen").GetComponent<KitchenScript>();
    }

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position - new Vector3(0.2f, 0.0f, 0.0f), Vector2.right * transform.localScale, rayDist);
        Collider2D grabbedObject = grabCheck.collider;
        if (grabbedObject != null && grabbedObject.tag == "Food" && !player.isHoldingFood)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                grabbedObject.gameObject.transform.parent = foodHolder;
                grabbedObject.gameObject.transform.position = foodHolder.position;
                grabbedObject.enabled = false;

                FoodScript foodScript = grabbedObject.GetComponent<FoodScript>();
                int foodId = foodScript.foodId;
                player.isHoldingFood = true;
                player.holdingFoodId = foodId;

                // Remove food from kitchen
                kitchenScript.PickUpOneFood(foodScript.foodInstanceId);
            }
            else
            {
                grabbedObject.gameObject.transform.parent = null;
                grabbedObject.enabled = true;
            }
        }
    }
}
