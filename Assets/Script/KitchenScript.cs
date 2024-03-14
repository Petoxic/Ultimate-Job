using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;

    public void OnInteract() {
        timerSlider.gameObject.SetActive(true);
    }
}
