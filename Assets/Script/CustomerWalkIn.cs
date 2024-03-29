using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomerWalkIn : MonoBehaviour
{
    private readonly float speed = 0.02f;
    [SerializeField] private GameObject target; // Chair customer will sit on
    [SerializeField] private float startingX; // Customer starting position x
    [SerializeField] private float startingY; // Customer starting position y
    // Customer starts walking to chair from out of the map
    private bool isSitting = false;
    private CustomerScript customerScript;
    [SerializeField] private float spawnDelay;
    private bool isSpawned = false;
    void Awake()
    {
        customerScript = gameObject.GetComponent<CustomerScript>();
        gameObject.transform.position = new Vector2(startingX, startingY);
        StartCoroutine(SpawnDelay());
    }
    // Customer walks to the chair
    void Update()
    {
        if (isSpawned && !isSitting)
            if (target.transform.position.x != gameObject.transform.position.x || target.transform.position.y != gameObject.transform.position.y)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed);
            }
            else
            {
                // If customer has reached the chair, start the order delay
                customerScript.OrderDelay();
                isSitting = true;
            }
    }
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        isSpawned = true;
    }
}
