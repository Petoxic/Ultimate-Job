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
    private Vector3Int gridChairPosition;

    void Awake()
    {
        int chairIdx = MapData.getChairPos();
        var (x, y) = MapData.chairPosition[chairIdx];
        if (DataManager.placedObjectsData.Count > 0)
        {
            chairIdx = Random.Range(0, DataManager.placedObjectsData.Count);
            int leftOrRight = Random.Range(0, 2);
            x = DataManager.placedObjectsData[chairIdx][leftOrRight].x;
            y = DataManager.placedObjectsData[chairIdx][leftOrRight].y;
        }

        gridChairPosition = new Vector3Int((int)x, (int)y, 0);

        customerScript = gameObject.GetComponent<CustomerScript>();
        gameObject.transform.position = MapData.outsideMapPosition;
        StartCoroutine(SpawnDelay());
    }
    // Customer walks to the chair
    void Update()
    {
        if (isSpawned && !isSitting)
            if (gameObject.transform.position.x != MapData.grid.CellToWorld(gridChairPosition).x
                || gameObject.transform.position.y != MapData.grid.CellToWorld(gridChairPosition).y)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, MapData.grid.CellToWorld(gridChairPosition), speed);
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
