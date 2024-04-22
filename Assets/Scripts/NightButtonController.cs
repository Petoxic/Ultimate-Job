using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    public static bool isStartRestrictAlert = false;

    void Update()
    {
        if (!UpgradeManager.isOpenUpgradeMenu && !ShopManager.isOpenShop && Input.GetKeyDown(KeyCode.Space))
        {
            if (DataManager.placedObjectsData.Count > 0)
            {
                DataManager.isDay = true;
                SceneManager.LoadScene("DayScene");
            }
            else
            {
                isStartRestrictAlert = true;
                Debug.Log("You should buy at least 1 table to start!");
            }
        }
    }
}
