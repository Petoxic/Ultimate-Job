using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private ShopInputManager shopInputManager;
    [SerializeField] private Grid gridMap1;
    [SerializeField] private Grid gridMap2;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private ShopItemSO[] shopItemsSO;
    [SerializeField] private PreviewSystem preview;
    [SerializeField] private ObjectPlacer objectPlacer;

    public static int topWallGridPosition;
    public static int bottomWallGridPosition;
    public static int leftWallGridPosition;
    public static int rightWallGridPosition;
    public static int kitchenGridPosition;
    public static bool isPlacement;
    public static bool isPreview;
    public static bool isRemoving;
    public static bool isUpgrade;
    public static Vector3Int gridSize;
    public static GameObject newObject;

    public event Action OnClicked, OnExit;
    private GridData furnitureData;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    private Grid grid;

    IBuildingState buildingState;

    private void Start()
    {
        topWallGridPosition = 10;
        bottomWallGridPosition = 2;
        leftWallGridPosition = 1;
        rightWallGridPosition = 26;
        kitchenGridPosition = 22;
        isPlacement = false;
        isPreview = false;
        isRemoving = false;
        isUpgrade = false;
        StopPlacement();
        furnitureData = new();
        gridSize = tilemap.GetComponent<Tilemap>().size;
        if (DataManager.caseNumber == 0)
        {
            grid = gridMap1;
        }
        else if (DataManager.caseNumber == 1)
        {
            grid = gridMap2;
        }
    }

    public void Update()
    {
        if (buildingState == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isRemoving && PlacementState.placementValidity)
            {
                if (DataManager.GetTotalMoney() < shopItemsSO[PlacementState.selectedObjectIndex].basePrice)
                {
                    Debug.Log("Your money is not enough");
                    return;
                }
                DataManager.SubMoney(shopItemsSO[PlacementState.selectedObjectIndex].basePrice);
                OnClicked?.Invoke();
            }
            if (isRemoving && RemovingState.removingValidity)
            {
                OnClicked?.Invoke();
            }
        }
        if (RemovingState.selectedRemoveIndex != -1)
        {
            DataManager.Refund(shopItemsSO[RemovingState.selectedRemoveIndex].basePrice);
            RemovingState.selectedRemoveIndex = -1;
        }
        if (isPlacement && (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape)))
        {
            OnExit?.Invoke();
        }

        if (ShopManager.isOpenShop)
        {
            buildingState.EndState();
        }

        Vector3 mousePosition = shopInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }

    public void StopPlacement()
    {
        if (buildingState == null)
        {
            return;
        }
        isPlacement = false;
        buildingState.EndState();
        this.OnClicked -= PlaceStructure;
        this.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        isPlacement = true;
        isRemoving = false;
        buildingState = new PlacementState(ID,
                                        grid,
                                        preview,
                                        shopItemsSO,
                                        furnitureData,
                                        objectPlacer);
        this.OnClicked += PlaceStructure;
        this.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        isRemoving = true;
        isPlacement = false;
        buildingState = new RemovingState(grid,
                                        preview,
                                        furnitureData,
                                        objectPlacer);
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

        buildingState.OnAction(gridPosition);
    }
}
