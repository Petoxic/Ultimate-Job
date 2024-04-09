using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawningScript : MonoBehaviour
{
    private static List<int> customerCountPerDay = new List<int>{3,3,4};
    private int customerCount = customerCountPerDay[DataManager.day-1];
    private float spawnDelay = 5.0f;
    private bool isReadyToSpawn = false;
    [SerializeField] private GameObject customerPrefab;

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        isReadyToSpawn = true;
    }

    void Start()
    {
        StartCoroutine(SpawnDelay());
    }

    void Update()
    {
        if (isReadyToSpawn && customerCount > 0)
        {
            isReadyToSpawn = false;
            Instantiate(customerPrefab, new Vector3((float)-0.231, (float)-1.266, 0), Quaternion.identity);
            StartCoroutine(SpawnDelay());
            customerCount -= 1;
        }
    }
}
