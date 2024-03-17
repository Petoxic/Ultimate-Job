using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public enum InteractDirection
    {
        up, down, left, right
    }
    public InteractDirection interactDirection;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    public PlayerInteract playerInteract;
    public List<int> orderList = new List<int>();

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();
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
            bool success = false;
            if (movementInput != Vector2.zero)
            {
                success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
                animator.SetInteger("moving", 0);
            }


            if (success && movementInput.x != 0.0f)
            {
                if (movementInput.x > 0)
                {
                    animator.SetInteger("moving", 4);
                }
                else if (movementInput.x < 0)
                {
                    animator.SetInteger("moving", 2);
                }

            }

            if (success && movementInput.y != 0.0f)
            {
                if (movementInput.y > 0)
                {
                    animator.SetInteger("moving", 3);
                }
                else
                {
                    animator.SetInteger("moving", 1);
                }
            }
        }


    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollision,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void OnInteract()
    {
        LockMovement();

        if (movementInput != Vector2.zero)
        {
            if (movementInput.x != 0.0)
            {
                if (movementInput.x > 0)
                {
                    playerInteract.InteractRight();
                }
                else if (movementInput.x < 0)
                {
                    playerInteract.InteractLeft();
                }

            }

            if (movementInput.y != 0.0)
            {
                if (movementInput.y > 0)
                {
                    playerInteract.InteractUp();
                }
                else
                {
                    playerInteract.InteractDown();
                }
            }
        }
        else
        {
            playerInteract.InteractDown();
        }
    }

    public void EndInteract()
    {
        UnlockMovement();
        playerInteract.StopInteract();
    }

    void OnFire()
    {
        animator.SetTrigger("interact");
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void AddOrder(int foodId)
    {
        orderList.Add(foodId);
    }

    public void ClearOrderList()
    {
        orderList.Clear();
    }
}