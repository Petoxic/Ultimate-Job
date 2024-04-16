using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private ShopInputManager shopInputManager;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private ShopItemSO[] shopItemsSO;
    [SerializeField] private PreviewSystem preview;
    [SerializeField] private ObjectPlacer objectPlacer;

    public static int wallGridPosition;
    public static int kitchenGridPosition;
    public static bool isPlacement;
    public static bool isPreview;
    public static bool isRemoving;
    public static Vector3Int gridSize;
    public static GameObject newObject;

    public event Action OnClicked, OnExit;
    private GridData furnitureData;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    IBuildingState buildingState;

    private void Start()
    {
        wallGridPosition = 11;
        kitchenGridPosition = 16;
        isPlacement = false;
        isPreview = false;
        isRemoving = false;
        StopPlacement();
        furnitureData = new();
        gridSize = tilemap.GetComponent<Tilemap>().size;
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
                DataManager.Refund(shopItemsSO[PlacementState.selectedObjectIndex].basePrice);
                OnClicked?.Invoke();
            }
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
