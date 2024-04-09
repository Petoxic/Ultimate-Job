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
            {"name", "Ms. Evelyn Sinclair - Wealthy socialite"},
            {"dialogue", new string[][] {
                new string[] {
                    "Oh, darling, did you hear about the theft at the museum? Dreadful business. I simply cannot fathom who would do such a thing. It's positively scandalous!"
                }
            }},
            {"foodAmount", 1},
        }},
        {2, new Dictionary<string, object> {
            {"name", "Mr. Daniel Blackwood - Eccentric art collector"},
            {"dialogue", new string[][] {
                new string[] {
                    "Art thefts, you say? Fascinating! Reminds me of that daring heist in Paris back in '98.",
                    "Ah, those were the days. If only I had been there to witness it firsthand!"
                },
            }},
            {"foodAmount", 1},
        }},
        {3, new Dictionary<string, object> {
            {"name", "Officer Jameson - Local police officer"},
            {"dialogue", new string[][] {
                new string[] {
                    "Another day, another crime scene. This city's getting worse by the minute.",
                    "You'd think with all the security measures in place, we'd catch these criminals red-handed. But no luck so far."
                },
            }},
            {"foodAmount", 1},
        }},
        {4, new Dictionary<string, object> {
            {"name", "Mrs. Margaret Chen - Museum curator"},
            {"dialogue", new string[][] {
                new string[] {
                    "The theft has shaken us all. The necklace was the centerpiece of our exhibit.",
                    "We've implemented strict security protocols, but it seems someone slipped through the cracks."
                }, 
            }},
            {"foodAmount", 1},
        }},
        {5, new Dictionary<string, object> {
            {"name", "Mr. Oliver Ford - Suspicious-looking man"},
            {"dialogue", new string[][] {
                new string[] {
                    "Me? Oh, I'm just here for the food. Don't mind me. Say, you wouldn't happen to be looking for someone, would you? Maybe I can help.",
                },
            }},
            {"foodAmount", 1},
        }},
        {6, new Dictionary<string, object> {
            {"name", "Ms. Lily Thompson - Aspiring artist"},
            {"dialogue", new string[][] {
                new string[] {
                    "I can't believe someone would steal from a museum. It's despicable! Those pieces belong to everyone, not just the rich and powerful.",
                    "If I ever caught the thief, I'd give them a piece of my mind!"
                },
            }},
            {"foodAmount", 1},
        }},
        {7, new Dictionary<string, object> {
            {"name", "Dr. Henry Wallace - Forensic scientist"},
            {"dialogue", new string[][] {
                new string[] {
                    "Analyzing the crime scene was like piecing together a puzzle. Every detail counts.",
                    "But even with all our high-tech equipment, we're struggling to find a lead. It's frustrating, to say the least."
                },
            }},
            {"foodAmount", 1},
        }},
        {8, new Dictionary<string, object> {
            {"name", "Mrs. Beatrice Ramirez - Local gossip"},
            {"dialogue", new string[][] {
                new string[] {
                    "Oh, honey, you wouldn't believe the rumors I've heard about that museum theft.",
                    "They say it was an inside job! Can you imagine? Someone from the staff betraying their own workplace? Scandalous, I tell you!"
                },
            }},
            {"foodAmount", 1},
        }},
        {9, new Dictionary<string, object> {
            {"name", "Mr. Charles Thompson - Retired detective"},
            {"dialogue", new string[][] {
                new string[] {
                    "I've seen my fair share of cases in my time. This one's got me stumped.",
                    "But mark my words, every criminal slips up eventually. It's just a matter of being patient and observant."
                },
            }},
            {"foodAmount", 1},
        }},
        {10, new Dictionary<string, object> {
            {"name", "Ms. Sophia Lee - Mysterious woman"},
            {"dialogue", new string[][] {
                new string[] {
                    "The necklace holds more than just monetary value. Its history is shrouded in secrets.",
                    "Whoever took it must have had a reason beyond simple greed. But what that reason is, I cannot say."
                },
            }},
            {"foodAmount", 1},
        }}
    };


    public static List<int> queue = Enumerable.Range(1, customerData.Count).ToList();
    public static int[] customerQueue = ShuffleArray(queue);
    public static int queuePos = 0;

    void Start()
    {
        queuePos = 0;
    }

    public static int getCustomerQueue()
    {

        return customerQueue[queuePos++];
    }

    public static int[] ShuffleArray(List<int> array)
    {
        System.Random random = new System.Random();
        return array.OrderBy(x => random.Next()).ToArray();
    }
}
