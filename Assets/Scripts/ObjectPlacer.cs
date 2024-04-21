using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        DataManager.placedGameObjects.Add(newObject);
        return DataManager.placedGameObjects.Count - 1;
    }

    public void RemoveObjectAt(int gameObjectIndex)
    {
        if (DataManager.placedGameObjects.Count <= gameObjectIndex || DataManager.placedGameObjects[gameObjectIndex] == null)
        {
            return;
        }
        Debug.Log(DataManager.placedGameObjects[gameObjectIndex]);
        Destroy(DataManager.placedGameObjects[gameObjectIndex]);
        DataManager.placedGameObjects[gameObjectIndex] = null;
    }
}
