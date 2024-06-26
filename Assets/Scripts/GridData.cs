using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridData
{
    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex, int selectedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData placementData = new PlacementData(positionToOccupy, ID, placedObjectIndex);

        Vector2Int leftChairPosition = CalculateChairPositions(gridPosition, 0, -1);
        Vector2Int rightChairPosition = CalculateChairPositions(gridPosition, objectSize.x - 1, -1);
        Vector2Int topLeftChairPosition = new Vector2Int(gridPosition.x, gridPosition.y);
        Vector2Int selectedObjectIndexTuple = new Vector2Int(selectedObjectIndex, selectedObjectIndex);

        List<Vector2Int> tableWithChairPosition = new List<Vector2Int> { leftChairPosition, rightChairPosition, topLeftChairPosition, selectedObjectIndexTuple };
        DataManager.placedObjectsData[placedObjectIndex] = tableWithChairPosition;

        foreach (var pos in positionToOccupy)
        {
            if (DataManager.placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position {pos}");
            }
            DataManager.placedObjects[pos] = placementData;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = -1; x < objectSize.x; x++)
        {
            for (int y = -1; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, -y, 0));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        // Check if object out of map
        int xHalfGridSize = PlacementSystem.gridSize.x / 2;
        int yHalfGridSize = PlacementSystem.gridSize.y / 2;
        if (gridPosition.x + xHalfGridSize > PlacementSystem.gridSize.x - objectSize.x
            || gridPosition.x + xHalfGridSize < PlacementSystem.leftWallGridPosition
            || gridPosition.y + yHalfGridSize > PlacementSystem.gridSize.y
            || gridPosition.y + yHalfGridSize < PlacementSystem.bottomWallGridPosition + objectSize.y
            || gridPosition.y + yHalfGridSize > PlacementSystem.topWallGridPosition
            || gridPosition.x + xHalfGridSize > PlacementSystem.rightWallGridPosition
            || gridPosition.x + xHalfGridSize + objectSize.x > PlacementSystem.kitchenGridPosition)
        {
            return false;
        }

        // Check if object occupied player
        if (PlacementSystem.gridPlayerPosition == gridPosition)
        {
            return false;
        }

        // Check if object will occupied by another object
        Vector2Int objectSizeRestrict = new Vector2Int(objectSize.x + 1, objectSize.y + 1);
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSizeRestrict);
        foreach (var pos in positionToOccupy)
        {
            if (DataManager.placedObjects.ContainsKey(pos)
                || PlacementSystem.gridPlayerPosition == pos
                || (PlacementSystem.gridPlayerPosition + new Vector3Int(0, 1, 0)) == pos
                || (PlacementSystem.gridPlayerPosition + new Vector3Int(0, -1, 0)) == pos)
            {
                return false;
            }
        }
        return true;
    }

    public int RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in DataManager.placedObjects[gridPosition].occupiedPositions)
        {
            DataManager.placedObjects.Remove(pos);
        }
        return -1;
    }

    public Vector2Int CalculateChairPositions(Vector3Int gridPosition, int xToChair, int yToChair)
    {
        Vector2Int chairPosition = new Vector2Int((int)(gridPosition.x + xToChair), (int)(gridPosition.y + yToChair));
        return chairPosition;
    }

    public int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (!DataManager.placedObjects.ContainsKey(gridPosition))
        {
            return -1;
        }
        return DataManager.placedObjects[gridPosition].PlacedObjectIndex;
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPosition, int id, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPosition;
        ID = id;
        PlacedObjectIndex = placedObjectIndex;
    }
}
