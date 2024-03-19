using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomerMoveTo : MonoBehaviour
{
    private readonly float speed = 0.02f;
    [SerializeField] private GameObject target; // Chair customer will sit on
    [SerializeField] private float startingX; // Customer starting position x
    [SerializeField] private float startingY; // Customer starting position y
    // Customer starts walking to chair from out of the map
    public bool isSitting = false; // Is customer sitting on the chair
    void Start()
    {
        gameObject.transform.position = new Vector2(startingX, startingY);
    }

    // Customer walks to chair
    void Update()
    {
        if (target.transform.position.x != gameObject.transform.position.x || target.transform.position.y != gameObject.transform.position.y)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed);
        }
        else
        {
            isSitting = true;
        }
    }
}
