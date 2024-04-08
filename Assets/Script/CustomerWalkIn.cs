using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomerWalkIn : MonoBehaviour
{
    private readonly float speed = 0.02f;
    private Vector2 chairPosition;
    // Customer starts walking to chair from out of the map
    private bool isSitting = false;
    private CustomerScript customerScript;
    [SerializeField] private float spawnDelay;
    private bool isSpawned = false;
    void Awake()
    {
        int chairIdx = MapData.getChairPos();
        var (x, y) = MapData.chairPosition[chairIdx];
        if (DataManager.placedObjectsData.Count > 0)
        {
            chairIdx = Random.Range(0, DataManager.placedObjectsData.Count);
            int leftOrRight = Random.Range(1, 3);
            x = DataManager.placedObjectsData[chairIdx][leftOrRight].x;
            y = DataManager.placedObjectsData[chairIdx][leftOrRight].y;

        }
        chairPosition = new Vector2((float)x, (float)y);

        customerScript = gameObject.GetComponent<CustomerScript>();
        gameObject.transform.position = MapData.outsideMapPosition;
        StartCoroutine(SpawnDelay());
    }
    // Customer walks to the chair
    void Update()
    {
        if (isSpawned && !isSitting)
            if (gameObject.transform.position.x != chairPosition.x || gameObject.transform.position.y != chairPosition.y)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, chairPosition, speed);
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
