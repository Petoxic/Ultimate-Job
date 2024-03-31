using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class CustomerData : MonoBehaviour
{
    public static Dictionary<int, Dictionary<string, object>> customerData = new Dictionary<int, Dictionary<string, object>>{
        {1, new Dictionary<string, object> {
            {"name", "Namo"},
            {"dialogue", new string[][] {
                new string[] {
                    "Yesterday, I sang this beautiful song at the club, and let me tell you, I was belting it out like a pro. But afterwards, I felt completely drained, like I'd run a marathon or something.",
                },
                new string[] {
                    "Possibly, but here's the weird part. When I woke up this morning, I swear my microphone felt bigger."
                }
            }},
            {"foodAmount", 2},
        }},
        {2, new Dictionary<string, object> {
            {"name", "Kanin"},
            {"dialogue", new string[][] {
                new string[] {
                    "You won't believe the day I had yesterday, I went out to buy a simple carrot for our family dinner, and somehow ended up at the nightclub instead of heading straight home.",
                    "When I finally made it home, ready to show off my prize carrot, it was nowhere to be found! Vanished into thin air."
                },
            }},
            {"foodAmount", 1},
        }},
        {3, new Dictionary<string, object> {
            {"name", "Nat"},
            {"dialogue", new string[][] {
                new string[] {
                    "It's me! Quick-witted, mischievous, and always ready to entertain.",
                    "I've heard someone gossip that I like to tease customers for fun. What do you think of me?"
                },
            }},
            {"foodAmount", 1},
        }},
    };

    public static List<int> customerQueue = Enumerable.Range(1, customerData.Count).ToList();
    public static int queuePos = 0;

    void Start() {
        queuePos = 0;
    }

    public static int getCustomerQueue()
    {
        return customerQueue[queuePos++];
    }
}
