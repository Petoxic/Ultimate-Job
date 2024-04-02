using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private ShopInputManager shopInputManager;

    private void Update()
    {
        Vector3 mousePosition = shopInputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
    }
}
