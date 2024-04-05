using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCount : MonoBehaviour
{
    public GameObject NightText;
    // Start is called before the first frame update
    void Start()
    {
        NightText.GetComponent<UnityEngine.UI.Text>().text = "Night " + DataManager.day;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
