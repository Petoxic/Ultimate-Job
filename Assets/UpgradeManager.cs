using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeMenu;
    public static bool isOpenUpgradeMenu;
    public UpgradeTemplate[] upgradePanels;
    public Button[] upgradeButtons;
    public GameObject undoObjectButton;

    void Start()
    {
        upgradeMenu.SetActive(false);
        isOpenUpgradeMenu = false;

        LoadPanels();
        CheckPurchaseable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isOpenUpgradeMenu)
            {
                undoObjectButton.SetActive(true);
                CloseUpgradeMenu();
            }
            else
            {
                undoObjectButton.SetActive(false);
                OpenUpgradeMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.B) && isOpenUpgradeMenu)
        {
            isOpenUpgradeMenu = false;
            undoObjectButton.SetActive(false);
            CloseUpgradeMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpenUpgradeMenu)
            {
                undoObjectButton.SetActive(true);
                CloseUpgradeMenu();
            }
        }
    }

    public void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        isOpenUpgradeMenu = true;
        Time.timeScale = 0f;
        CheckPurchaseable();
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        isOpenUpgradeMenu = false;
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < upgradePanels.Length; i++)
        {
            int price = 0;
            switch (i)
            {
                case 0:
                    price = 10 + ((DataManager.moveLevels - 1) * 10);
                    break;
                case 1:
                    price = 10 + ((DataManager.cookingLevels - 1) * 10);
                    break;
            }
            if (DataManager.GetTotalMoney() >= price && DataManager.GetDay() > 0)
            {
                upgradeButtons[i].interactable = true;
            }
            else
            {
                upgradeButtons[i].interactable = false;
            }
        }
    }

    public void UpgradeItem(int itemNumber)
    {
        switch (itemNumber)
        {
            case 0:
                int price = 10 + ((DataManager.moveLevels - 1) * 10);
                if (DataManager.GetTotalMoney() >= price)
                {
                    DataManager.moveLevels += 1;
                    DataManager.SubMoney(price);
                }
                break;
            case 1:
                int price1 = 10 + ((DataManager.cookingLevels - 1) * 10);
                if (DataManager.GetTotalMoney() >= price1)
                {
                    DataManager.cookingLevels += 1;
                    DataManager.SubMoney(price1);
                }
                break;
        }
        CheckPurchaseable();
        LoadPanels();
    }


    public void LoadPanels()
    {
        for (int i = 0; i < upgradePanels.Length; i++)
        {
            switch (i)
            {
                case 0:
                    upgradePanels[i].titleText.text = "Moving speed";
                    upgradePanels[i].priceText.text = "Coin: " + (10 + ((DataManager.moveLevels - 1) * 10)) + " $";
                    upgradePanels[i].levelText.text = "Level: " + DataManager.moveLevels;
                    break;
                case 1:
                    upgradePanels[i].titleText.text = "Cooking speed";
                    upgradePanels[i].priceText.text = "Coin: " + (10 + ((DataManager.cookingLevels - 1) * 10)) + " $";
                    upgradePanels[i].levelText.text = "Level: " + DataManager.cookingLevels;
                    break;
            }
        }
    }
}