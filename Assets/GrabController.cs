using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform foodHolder;
    public float rayDist;

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        print("test " + grabCheck.collider.tag);
        if(grabCheck.collider != null && grabCheck.collider.tag == "Food") {
            if (Input.GetKey(KeyCode.G)) {
                grabCheck.collider.gameObject.transform.parent = foodHolder;
                grabCheck.collider.gameObject.transform.position = foodHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            } else {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;                
            }
        }
    }
}
