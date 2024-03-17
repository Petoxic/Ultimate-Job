using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Collider2D interactCollider;
    private PlayerScript player;
    private KitchenScript kitchen;
    Vector2 interactOffset;

    private void Start()
    {
        interactOffset = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        kitchen = GameObject.FindGameObjectWithTag("Kitchen").GetComponent<KitchenScript>();
    }
    public void InteractRight()
    {
        print("interact right");
        interactCollider.enabled = true;
        transform.localPosition = new Vector3(interactOffset.x + 0.2f, interactOffset.y + 0.2f);
    }

    public void InteractLeft()
    {
        // print("interact left");
        interactCollider.enabled = true;
        transform.localPosition = new Vector3(interactOffset.x - 0.2f, interactOffset.y + 0.2f);
    }

    public void InteractUp()
    {
        // print("interact up");
        interactCollider.enabled = true;
        transform.localPosition = new Vector3(interactOffset.x, interactOffset.y * -1);
    }

    public void InteractDown()
    {
        // print("interact down");
        interactCollider.enabled = true;
        transform.localPosition = interactOffset;
    }

    public void StopInteract()
    {
        // print("endd");
        interactCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // print(interactOffset);
        // print("test" + other.tag + other.name);
        if (other.tag == "Customer")
        {
            CustomerScript customer = other.GetComponent<CustomerScript>();
            customer.OnInteract();
        }
        else if (other.tag == "Kitchen")
        {
            KitchenScript kitchen = other.GetComponent<KitchenScript>();
            kitchen.OnInteract();
        }
        else if (other.tag == "Food")
        {
            // kitchen remove food && player take food
        }
    }
}
