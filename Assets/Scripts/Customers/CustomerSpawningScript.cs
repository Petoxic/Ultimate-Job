using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawningScript : MonoBehaviour
{
    private static readonly List<int> customerCountPerDay = new() { 3, 3, 4 };
    private int remainingCustomerCount;
    public static int customerCountInMap;
    private readonly float spawnDelay = 5.0f; // TODO: randomize this value
    private bool isReadyToSpawn = false;
    [SerializeField] private GameObject customerPrefab;

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        isReadyToSpawn = true;
    }

    void Start()
    {
        remainingCustomerCount = customerCountPerDay[DataManager.GetDay()];
        customerCountInMap = 0;
        StartCoroutine(SpawnDelay());
    }

    void Update()
    {
        bool isFull = customerCountInMap == DataManager.placedGameObjects.Count * 2;
        if (isReadyToSpawn && remainingCustomerCount > 0 && !isFull)
        {
            isReadyToSpawn = false;
            Instantiate(customerPrefab, new Vector3((float)-0.231, (float)-1.266, 0), Quaternion.identity);
            customerCountInMap += 1;
            StartCoroutine(SpawnDelay());
            remainingCustomerCount -= 1;
            Debug.Log("remaining customers count " + remainingCustomerCount);
        }
    }

    //find min
    private static int Min(int a, int b)
    {
        return a < b ? a : b;
    }
}
