using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    public static bool removingValidity;
    public static int selectedRemoveIndex = -1;

    public RemovingState(Grid grid, PreviewSystem previewSystem, GridData furnitureData, ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        ShopManager.isOpenShop = false;

        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPlacementPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        PlacementSystem.isRemoving = false;
        if (!furnitureData.CanRemoveObjectAt(gridPosition, Vector2Int.one))
        {
            selectedData = furnitureData;
        }
        if (selectedData == null)
        {
            Debug.Log("You IDIOT!");
        }
        else
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
            {
                Debug.Log("You SUCK!");
                return;
            }
            PlacementSystem.isRemoving = true;
            selectedRemoveIndex = DataManager.placedObjectsData[gameObjectIndex][3].x;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition));
    }

    public bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        if (!furnitureData.CanRemoveObjectAt(gridPosition, Vector2Int.one))
        {
            removingValidity = true;
        }
        else
        {
            removingValidity = false;
        }
        return removingValidity;
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }
}
