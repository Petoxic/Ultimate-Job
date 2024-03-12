using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int movementSpeed = 10;
    
    private Vector2 movementDirection;
    public Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate() {
        rb.velocity = movementSpeed * movementDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("โอ๊ย " + name + " hit " + collision.gameObject.name);
    }

}