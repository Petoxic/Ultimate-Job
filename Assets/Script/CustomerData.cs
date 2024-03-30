using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerData : MonoBehaviour
{
    public static Dictionary<int, object> customerData = new Dictionary<int, object>{
        {1, new Dictionary<string, object> {
            {"name", "Kanin"},
            {"dialogue", new string[] {"Hi, I am Kanin", "I am very handsome", "I am the most handsome man in the world"}},
            {"foodAmount", 1}
        }}
    };
}
