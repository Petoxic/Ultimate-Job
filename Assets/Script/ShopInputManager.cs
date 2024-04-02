using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector3 lastPosition;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 lastPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lastPosition.z = 0f;
        return lastPosition;
    }
}
