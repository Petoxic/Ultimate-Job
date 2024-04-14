using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public static Vector2 outsideMapPosition = new Vector2((float)-0.231, (float)-1.266);
    public static (double, double)[] chairPosition = new (double, double)[] {
        (-0.882, -0.37), // left table left
        (-0.26, -0.37), // left table right
        (-0.278, 0.588), // 3rd box
        (0.726, -0.52), // right table left
        (1.352, -0.5), // right table right
        (-0.704, 0.36), // 2nd box
    };

    public static int queuePos = 0;

    [SerializeField] public Grid gridInput;
    public static Grid grid;
    public int selectedObjectIndex;

    [SerializeField] private ShopItemSO[] shopItemsSO;

    void Start()
    {
        selectedObjectIndex = 0;
        queuePos = 0;
        grid = gridInput;

        for (int i = 0; i < DataManager.placedObjectsData.Count; i++)
        {
            Vector3Int chairPos = new Vector3Int(DataManager.placedObjectsData[i][2].x,
                                                DataManager.placedObjectsData[i][2].y,
                                                0);
            GameObject newPlacedObject = Instantiate(shopItemsSO[DataManager.placedObjectsData[i][3].x].Prefab);
            newPlacedObject.transform.position = grid.CellToWorld(chairPos);
        }
    }

    public static int getChairPos()
    {
        return queuePos++;
    }
}
