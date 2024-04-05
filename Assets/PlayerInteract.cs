using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Collider2D interactCollider;
    Vector2 interactOffset;

    private void Start()
    {
        interactOffset = transform.position;
    }

    public void Interact()
    {
        interactCollider.enabled = true;
    }

    public void FaceRight()
    {
        transform.localPosition = new Vector3(interactOffset.x + 0.2f, interactOffset.y + 0.2f);
    }

    public void FaceLeft()
    {
        transform.localPosition = new Vector3(interactOffset.x - 0.2f, interactOffset.y + 0.2f);
    }

    public void FaceUp()
    {
        transform.localPosition = new Vector3(interactOffset.x, interactOffset.y * -1);
    }

    public void FaceDown()
    {
        transform.localPosition = interactOffset;
    }

    public void StopInteract()
    {
        interactCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // print(interactOffset);
        // print("test" + other.tag + other.name);
        if (other.CompareTag("Customer"))
        {
            CustomerScript customer = other.GetComponent<CustomerScript>();
            customer.OnInteract();
        }
        else if (other.CompareTag("Kitchen"))
        {
            KitchenScript kitchen = other.GetComponent<KitchenScript>();
            kitchen.OnInteract();
        }
        else if (other.CompareTag("Food"))
        {
            // kitchen remove food && player take food
        }
        else if (other.CompareTag("Bin"))
        {
            BinScript bin = other.GetComponent<BinScript>();
            bin.OnInteract();
        }
    }
}
