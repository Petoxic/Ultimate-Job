using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseCount : MonoBehaviour
{
    public GameObject CaseText;
    // Start is called before the first frame update
    void Start()
    {
        CaseText.GetComponent<UnityEngine.UI.Text>().text = "Case " + DataManager.caseNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
