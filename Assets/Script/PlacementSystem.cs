using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private ShopInputManager shopInputManager;
    [SerializeField] private Grid grid;

    [SerializeField] public ShopItemSO[] shopItemsSO;
    public int selectedObjectIndex = -1;

    public static bool isPlacement;
    public event Action OnClicked, OnExit;

    private void Start()
    {
        isPlacement = false;
        StopPlacement();
    }

    public void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (DataManager.totalMoney < shopItemsSO[selectedObjectIndex].basePrice)
            {
                Debug.Log("Your money is not enough");
                return;
            }
            DataManager.SubMoney(shopItemsSO[selectedObjectIndex].basePrice);
            OnClicked?.Invoke();
        }
        if (isPlacement && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.B)))
        {
            OnExit?.Invoke();
        }

        Vector3 mousePosition = shopInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

    public void StopPlacement()
    {
        isPlacement = false;
        selectedObjectIndex = -1;
        cellIndicator.SetActive(false);
        OnClicked -= PlaceStructure;
        OnExit -= StopPlacement;
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
        cellIndicator.SetActive(true);
        OnClicked += PlaceStructure;
        OnExit += StopPlacement;
    }

    public void PlaceStructure()
    {
        if (shopInputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = shopInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        GameObject newObject = Instantiate(shopItemsSO[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);

    }
}
