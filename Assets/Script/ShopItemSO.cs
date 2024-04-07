using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "ScriptableObjects/New Shop Item", order = 0)]
public class ShopItemSO : ScriptableObject
{
    public int ID;
    public string title;
    public string description;
    public int basePrice;
    public Vector2Int size = Vector2Int.one;
    public GameObject Prefab;
}