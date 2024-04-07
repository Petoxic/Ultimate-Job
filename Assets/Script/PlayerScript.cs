using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private static class AnimatorParameters
    {
        public const string IsMoving = "IsMoving";
        public const string X = "X";
        public const string Y = "Y";
        public const string Interact = "Interact";
    }
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    // public ContactFilter2D movementFilter;
    public PlayerInteract playerInteract;
    public List<int> orderList = new();
    public bool isHoldingFood;
    public int holdingFoodId;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movementInput);
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if (movementInput != Vector2.zero)
        {
            animator.SetFloat(AnimatorParameters.X, movementInput.x);
            animator.SetFloat(AnimatorParameters.Y, movementInput.y);

            animator.SetBool(AnimatorParameters.IsMoving, true);

            // Face the player to the direction of movement 
            if (movementInput.x > 0)
            {
                playerInteract.FaceRight();
            }
            else if (movementInput.x < 0)
            {
                playerInteract.FaceLeft();
            }
            else if (movementInput.y > 0)
            {
                playerInteract.FaceUp();
            }
            else if (movementInput.y < 0)
            {
                playerInteract.FaceDown();
            }
        }
        else
        {
            animator.SetBool(AnimatorParameters.IsMoving, false);
        }
    }

    public void OnInteract()
    {
        LockMovement();
        playerInteract.Interact();
    }

    public void EndInteract()
    {
        UnlockMovement();
        playerInteract.StopInteract();
    }

    void OnFire()
    {
        animator.SetTrigger(AnimatorParameters.Interact);
    }

    private void LockMovement()
    {
        canMove = false;
    }

    private void UnlockMovement()
    {
        canMove = true;
    }

    public void AddOrder(int foodId)
    {
        orderList.Add(foodId);
    }

    public void ServeOrder(int foodId)
    {
        orderList.RemoveAll(id => id == foodId);
        // todo: fix bug all food was gone after serve
        GameObject holdObject = gameObject.transform.Find("HoldObject").gameObject;
        GameObject foundItem = holdObject.transform.Find("Food(Clone)").gameObject;
        Destroy(foundItem);
    }

    public void ClearOrderList()
    {
        orderList.Clear();
    }
}