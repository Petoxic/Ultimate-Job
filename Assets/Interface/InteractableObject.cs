using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private bool isInteracted = false;

    protected virtual void OnCollisionStay2D() {
        if (Input.GetKey(KeyCode.E)) {
            OnInteract();
        }
    }

    protected virtual void OnInteract() {
        if (!isInteracted) {
            InteractAction();
            isInteracted = true;
        }
    }

    protected virtual void InteractAction() {
        Debug.Log("default interact");
    }

    private void OnCollisionExit2D() {
        isInteracted = false;
    }
}