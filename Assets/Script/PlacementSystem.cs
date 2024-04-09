using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private ShopInputManager shopInputManager;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private ShopItemSO[] shopItemsSO;
    [SerializeField] private PreviewSystem preview;

    public int selectedObjectIndex = -1;

    public static int wallGridPosition;
    public static bool isPlacement;
    public static bool placementValidity;
    public static Vector3Int gridSize;

    public event Action OnClicked, OnExit;
    private GridData furnitureData;
    private List<GameObject> placedGameObjects = new();
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        wallGridPosition = 8;
        isPlacement = false;
        StopPlacement();
        furnitureData = new();
        gridSize = tilemap.GetComponent<Tilemap>().size;
    }

    public void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (placementValidity)
            {
                if (DataManager.totalMoney < shopItemsSO[selectedObjectIndex].basePrice)
                {
                    Debug.Log("Your money is not enough");
                    return;
                }
                DataManager.SubMoney(shopItemsSO[selectedObjectIndex].basePrice);
                OnClicked?.Invoke();
            }
        }
        if (isPlacement && Input.GetKeyDown(KeyCode.B))
        {
            OnExit?.Invoke();
        }

        Vector3 mousePosition = shopInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

            mouseIndicator.transform.position = mousePosition;
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            lastDetectedPosition = gridPosition;
        }
    }

    public void StopPlacement()
    {
        isPlacement = false;
        selectedObjectIndex = -1;
        preview.StopShowingPlacementReview();
        this.OnClicked -= PlaceStructure;
        this.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        isPlacement = true;
        selectedObjectIndex = shopItemsSO[ID].ID;
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        preview.StartShowingPlacementReview(shopItemsSO[ID].Prefab, shopItemsSO[ID].size);
        this.OnClicked += PlaceStructure;
        this.OnExit += StopPlacement;
    }

    public void PlaceStructure()
    {
        if (shopInputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = shopInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!placementValidity)
        {
            return;
        }

        GameObject newObject = Instantiate(shopItemsSO[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
        placedGameObjects.Add(newObject);
        GridData selectedData = furnitureData;
        selectedData.AddObjectAt(gridPosition,
                                shopItemsSO[selectedObjectIndex].size,
                                shopItemsSO[selectedObjectIndex].ID,
                                placedGameObjects.Count - 1);
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
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
}
