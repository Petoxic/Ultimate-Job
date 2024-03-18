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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        // print("test " + grabCheck.collider.tag);
        Collider2D grabbedObject = grabCheck.collider;
        if (grabbedObject != null && grabbedObject.tag == "Food" && !player.isHoldingFood)
        {
            if (Input.GetKey(KeyCode.G))
            {
                grabbedObject.gameObject.transform.parent = foodHolder;
                grabbedObject.gameObject.transform.position = foodHolder.position;
                grabbedObject.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.enabled = false;
                int foodId = grabbedObject.GetComponent<FoodScript>().foodId;
                player.isHoldingFood = true;
                player.holdingFoodId = foodId;
            }
            else
            {
                grabbedObject.gameObject.transform.parent = null;
                grabbedObject.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.enabled = true;
            }
        }
    }
}
