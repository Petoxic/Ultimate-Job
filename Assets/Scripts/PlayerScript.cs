using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
// using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private GameObject floatingTextPrefab;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;

    [SerializeField] private GameObject orderNote;

    // public static PlayerScript Instance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (DataManager.isDay == false)
        {
            orderNote.SetActive(false);
        }

        // Set persist player position 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = DataManager.playerPosition;
        }
    }

    void FixedUpdate()
    {
        DataManager.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (canMove && !DataManager.startTalking)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movementInput);
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if (movementInput != Vector2.zero && !DataManager.startTalking)
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
        orderNote.SetActive(true);
        orderList.Add(foodId);
    }

    public void PlaceOrder()
    {
        orderNote.SetActive(false);
    }

    private void ShowFloatingText(String text) {
        var go = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 0.18f, 0), Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = text;
    }

    public void ServeOrder(int foodId, String text)
    {
        orderList.RemoveAll(id => id == foodId);
        // todo: fix bug all food was gone after serve
        GameObject holdObject = gameObject.transform.Find("HoldObject").gameObject;
        GameObject foundItem = holdObject.transform.Find("Food(Clone)").gameObject;
        ShowFloatingText(text);
        Destroy(foundItem);
    }

    public void ClearOrderList()
    {
        orderList.Clear();
    }
}