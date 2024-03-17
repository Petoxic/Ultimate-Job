using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColor : MonoBehaviour
{
    public Text header;
    public Text colorchangingFont1;
    public Text colorchangingFont2;
    public Text colorchangingFont3;
    List<bool> names = new List<bool>() { true, true, true };


    // Start is called before the first frame update
    void Start()
    {
        if  (names[0] && names[1] && names[2]){
            header.text = "Done!";
        }else{
            header.text = "Game Over!";
        }
        if(names[0]){
            colorchangingFont1.color = Color.yellow;
        }else{
            colorchangingFont1.color = Color.red;
        }
        if(names[1]){
            colorchangingFont2.color = Color.yellow;
        }else{
            colorchangingFont2.color = Color.red;
        }
        if(names[2]){
            colorchangingFont3.color = Color.yellow;
        }else{
            colorchangingFont3.color = Color.red;
        }

        
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
