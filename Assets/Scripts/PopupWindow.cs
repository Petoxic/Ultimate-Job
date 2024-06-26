using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    public TMP_Text popupText;

    private GameObject window;
    private Animator popupAnimator;
    private Queue<string> popupQueue;
    private Coroutine queueChecker;


    public void Start()
    {
        window = transform.GetChild(0).gameObject;
        popupAnimator = window.GetComponent<Animator>();
        window.SetActive(false);
        popupQueue = new Queue<string>();
    }

    public void Update()
    {
        if (GoToNextScene.isStartRestrictAlert)
        {
            popupQueue.Clear();
            window.SetActive(false);
            queueChecker = null;
            AddToQueue("At least one table must be purchased to start the day.");
        }
        if (ShopManager.isOpenShop || UpgradeManager.isOpenUpgradeMenu)
        {
            popupQueue.Clear();
            window.SetActive(false);
            queueChecker = null;
        }
        GoToNextScene.isStartRestrictAlert = false;
    }

    public void AddToQueue(string text)
    {
        popupQueue.Enqueue(text);
        if (queueChecker == null)
        {
            queueChecker = StartCoroutine(CheckQueue());
        }
    }

    public void ShowPopup(string text)
    {
        window.SetActive(true);
        popupText.text = text;
        popupAnimator.Play("PopupAnimation");
    }

    private IEnumerator CheckQueue()
    {
        do
        {
            ShowPopup(popupQueue.Dequeue());
            do
            {
                yield return null;
            } while (!popupAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));
        } while (popupQueue.Count > 0);
        window.SetActive(false);
        queueChecker = null;
    }
}
