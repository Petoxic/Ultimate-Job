using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenScript : InteractableObject
{
    protected override void InteractAction()
    {
        Debug.Log("Interact with " + name);
    }
}
