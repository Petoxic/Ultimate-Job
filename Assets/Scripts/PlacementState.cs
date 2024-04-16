using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int ID;
    private Grid grid;
    private PreviewSystem previewSystem;
    private ShopItemSO[] shopItemsSO;
    private GridData furnitureData;
    private ObjectPlacer objectPlacer;

    private int index;

    public static int selectedObjectIndex = -1;
    public static bool placementValidity;

    public PlacementState(int iD,
                        Grid grid,
                        PreviewSystem previewSystem,
                        ShopItemSO[] shopItemsSO,
                        GridData furnitureData,
                        ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.shopItemsSO = shopItemsSO;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = shopItemsSO[iD].ID;
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(shopItemsSO[iD].Prefab, shopItemsSO[iD].size);
        }
        else
        {
            throw new System.Exception($"No object with ID {iD}");
        }
    }

    public void EndState()
    {
        previewSystem.StopShowingPlacementPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!placementValidity)
        {
            return;
        }

        index = objectPlacer.PlaceObject(shopItemsSO[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));
        GridData selectedData = furnitureData;
        selectedData.AddObjectAt(gridPosition,
                                shopItemsSO[selectedObjectIndex].size,
                                shopItemsSO[selectedObjectIndex].ID,
                                DataManager.placedObjectsData.Count,
                                selectedObjectIndex);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        if (selectedObjectIndex < 0)
        {
            return false;
        }
        GridData selectedData = furnitureData;
        return selectedData.CanPlaceObjectAt(gridPosition, shopItemsSO[selectedObjectIndex].size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}
