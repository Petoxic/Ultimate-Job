using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopMenu;
    public ShopItemSO[] shopItemsSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseButtons;
    public static bool isOpenShop;
    public GameObject removeObjectButton;

    void Start()
    {
        shopMenu.SetActive(false);
        isOpenShop = false;

        LoadPanels();
        CheckPurchaseable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isOpenShop)
            {
                removeObjectButton.SetActive(true);
                CloseShopMenu();
            }
            else
            {
                removeObjectButton.SetActive(false);
                OpenShopMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.U) && isOpenShop)
        {
            isOpenShop = false;
            removeObjectButton.SetActive(false);
            CloseShopMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpenShop)
            {
                removeObjectButton.SetActive(true);
                CloseShopMenu();
            }
        }

        if (PlacementSystem.isRemoving)
        {
            removeObjectButton.SetActive(true);
            CloseShopMenu();
        }
    }

    public void OpenShopMenu()
    {
        shopMenu.SetActive(true);
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        Time.timeScale = 0f;
        isOpenShop = true;
        PlacementSystem.isRemoving = false;
        CheckPurchaseable();
    }

    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(false);
        }
        Time.timeScale = 1f;
        isOpenShop = false;
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (DataManager.GetTotalMoney() >= shopItemsSO[i].basePrice)
            {
                myPurchaseButtons[i].interactable = true;
            }
            else
            {
                myPurchaseButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int buttonNumber)
    {
        if (DataManager.GetTotalMoney() >= shopItemsSO[buttonNumber].basePrice)
        {
            CloseShopMenu();
            CheckPurchaseable();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleText.text = shopItemsSO[i].title;
            shopPanels[i].descriptionText.text = shopItemsSO[i].description;
            shopPanels[i].priceText.text = "Coins: " + shopItemsSO[i].basePrice.ToString();
        }
    }
}
