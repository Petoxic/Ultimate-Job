using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomerWalkIn : MonoBehaviour
{
    private readonly float speed = 0.02f;
    private Vector2 chairPosition;
    private Vector3 topLeftPosition;
    private Vector3 gridCellSize;
    private Vector2 centerPosition;
    // Customer starts walking to chair from out of the map
    private bool isSitting = false;
    private CustomerScript customerScript;
    [SerializeField] private float spawnDelay;
    private bool isSpawned = false;
    private Vector3Int gridChairPosition;
    private double x, y;

    public static int tableIdx;
    public static int leftOrRight;

    void Awake()
    {
        if (DataManager.placedObjectsData.Count > 0)
        {
            if (tableIdx == DataManager.placedObjectsData.Count)
            {
                tableIdx = 0;
            }

            Debug.Log(DataManager.placedObjectsData.Count + " " + tableIdx + " " + leftOrRight);
            x = DataManager.placedObjectsData[tableIdx][leftOrRight].x;
            y = DataManager.placedObjectsData[tableIdx][leftOrRight].y;
            if (leftOrRight == 1)
            {
                leftOrRight--;
                tableIdx++;
            }
            else
            {
                leftOrRight++;
            }
        }

        gridChairPosition = new Vector3Int((int)x, (int)y, 0);
        Debug.Log("Grid Position: " + gridChairPosition);

        customerScript = gameObject.GetComponent<CustomerScript>();
        gameObject.transform.position = MapData.outsideMapPosition;
        StartCoroutine(SpawnDelay());
    }
    // Customer walks to the chair
    void Update()
    {
        if (isSpawned && !isSitting)
        {
            topLeftPosition = MapData.grid.CellToWorld(gridChairPosition);
            gridCellSize = MapData.grid.cellSize;
            centerPosition = new Vector2(topLeftPosition.x + gridCellSize.x / 2, topLeftPosition.y);
            if (gameObject.transform.position.x != centerPosition.x
                || gameObject.transform.position.y != centerPosition.y)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, centerPosition, speed);
            }
            else
            {
                // If customer has reached the chair, start the order delay
                customerScript.OrderDelay();
                isSitting = true;
            }
        }
    }
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        isSpawned = true;
    }
}
