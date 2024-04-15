using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    void Update()
    {
        if (!ShopManager.isOpenShop && Input.GetKeyDown(KeyCode.Space))
        {
            if (DataManager.placedObjectsData.Count > 0)
            {
                SceneManager.LoadScene("DayScene");
            }
            else
            {
                Debug.Log("You should buy at least 1 table to start!");
            }
        }
    }
}
