using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopInputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector3 lastPosition;

    public void Update()
    {

    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 lastPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lastPosition.z = 0f;
        return lastPosition;
    }
}
