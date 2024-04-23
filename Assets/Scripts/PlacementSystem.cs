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
    public static bool isUpgrade;
    public static bool isUndo;
    public static Vector3Int gridSize;
    public static GameObject newObject;
    public static Vector3Int gridPlayerPosition;

    public event Action OnClicked, OnExit;
    private GridData furnitureData;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    private Grid grid;
    public GameObject undoObjectButton;

    IBuildingState buildingState;

    private void Start()
    {
        topWallGridPosition = 11;
        bottomWallGridPosition = 2;
        leftWallGridPosition = 1;
        rightWallGridPosition = 26;
        kitchenGridPosition = 22;
        isPlacement = false;
        isPreview = false;
        isUpgrade = false;
        isUndo = false;
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
        gridPlayerPosition = grid.WorldToCell(DataManager.playerPosition);
    }

    public void Update()
    {
        gridPlayerPosition = grid.WorldToCell(DataManager.playerPosition);

        if (buildingState == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (PlacementState.placementValidity)
            {
                if (DataManager.GetTotalMoney() < shopItemsSO[PlacementState.selectedObjectIndex].basePrice)
                {
                    Debug.Log("Your money is not enough");
                    return;
                }
                DataManager.SubMoney(shopItemsSO[PlacementState.selectedObjectIndex].basePrice);
                OnClicked?.Invoke();
            }
        }
        if (isPlacement && (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape)))
        {
            isPlacement = false;
            undoObjectButton.SetActive(false);
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
        buildingState.EndState();
        this.OnClicked -= PlaceStructure;
        this.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        undoObjectButton.SetActive(true);
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

    public void UndoPlacement()
    {
        isUndo = true;
        List<int> placedObjectsDataKeys = new List<int>(DataManager.placedObjectsData.Keys);
        List<Vector3Int> placedObjectsKeys = new List<Vector3Int>(DataManager.placedObjects.Keys);

        int placedObjectsDataLastKey = placedObjectsDataKeys[placedObjectsDataKeys.Count - 1];
        Vector3Int placedObjectsLastKeys = placedObjectsKeys[placedObjectsKeys.Count - 1];

        List<Vector2Int> lastElement = DataManager.placedObjectsData[placedObjectsDataLastKey];
        DataManager.Refund(shopItemsSO[lastElement[3].x].basePrice);

        UndoObjectAt(DataManager.placedObjectsData.Count - 1);

        DataManager.placedObjectsData.Remove(placedObjectsDataLastKey);
        furnitureData.RemoveObjectAt(new Vector3Int(lastElement[2].x, lastElement[2].y, 0));
    }

    public void UndoObjectAt(int gameObjectIndex)
    {
        if (DataManager.placedGameObjects.Count <= gameObjectIndex || DataManager.placedGameObjects[gameObjectIndex] == null)
        {
            return;
        }
        Destroy(DataManager.placedGameObjects[gameObjectIndex]);
        DataManager.placedGameObjects[gameObjectIndex] = null;
        DataManager.placedGameObjects.RemoveAt(gameObjectIndex);
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
