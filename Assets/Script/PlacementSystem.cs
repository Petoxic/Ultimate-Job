using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private ShopInputManager shopInputManager;
    [SerializeField] private Grid grid;

    [SerializeField] private ShopItemSO[] shopItemsSO;
    private int selectedObjectIndex = -1;

    public static bool isPlacement;

    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        // ShopManager.shopMenu.SetActive(false);
        StopPlacement();
        selectedObjectIndex = shopItemsSO[ID].ID;
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        cellIndicator.SetActive(true);
        shopInputManager.OnClicked += PlaceStructure;
        shopInputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
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

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        cellIndicator.SetActive(false);
        shopInputManager.OnClicked -= PlaceStructure;
        shopInputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        // if (selectedObjectIndex < 0)
        // {
        //     return;
        // }
        Vector3 mousePosition = shopInputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
