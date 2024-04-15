using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class CustomerData : MonoBehaviour
{
    public static Dictionary<int, Dictionary<string, object>> customers = new()
    {
        {1, new Dictionary<string, object> {
            {"name", "Mrs. Evelyn Sinclair - Rich lady"},
            {"dialogue", new string[][] {
                new string[] {
                    "Oh my, did you hear? Someone swiped a necklace from the museum. Horrid stuff. Who'd do such a thing? It's just awful!"
                }
            }},
            {"foodAmount", 1},
        }},
        {2, new Dictionary<string, object> {
            {"name", "Mr. Daniel Blackwood - Art fan"},
            {"dialogue", new string[][] {
                new string[] {
                    "Art thefts, you say? Fascinating! Reminds me of that daring heist in Paris back in '98.",
                    "Wish I'd been there! What a story that would've been!"
                },
            }},
            {"foodAmount", 1},
        }},
        {3, new Dictionary<string, object> {
            {"name", "Officer Jameson - Local cop"},
            {"dialogue", new string[][] {
                new string[] {
                    "Another day, another crime scene. This city's getting worse by the minute.",
                    "You'd think with all the security measures in place, we'd catch these criminals red-handed. But no luck so far."
                },
            }},
            {"foodAmount", 1},
        }},
        {4, new Dictionary<string, object> {
            {"name", "Mrs. Margaret Chen - Museum boss"},
            {"dialogue", new string[][] {
                new string[] {
                    "The theft has shaken us all. That necklace was the star of our show.",
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
                    "I can't believe someone would steal from a museum. It's shameful! Those pieces belong to everyone, not just the rich and powerful.",
                    "If I ever caught the thief, they'd hear about it!"
                },
            }},
            {"foodAmount", 1},
        }},
        {7, new Dictionary<string, object> {
            {"name", "Dr. Henry Wallace - Science dude"},
            {"dialogue", new string[][] {
                new string[] {
                    "Crime scene was like solving a puzzle. Every bit matters.",
                    "But even with our gadgets, we're lost. It's tough."
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
                    "I've seen it all, but this one's tricky.",
                    "Criminals slip up, though. Just gotta wait and watch. We'll get 'em."
                },
            }},
            {"foodAmount", 1},
        }},
        {10, new Dictionary<string, object> {
            {"name", "Ms. Sophia Lee - Mysterious woman"},
            {"dialogue", new string[][] {
                new string[] {
                    "That necklace? Holds more than just cash value. It's got a story. ",
                    "Whoever took it had a reason, a big one. But what? No clue."
                },
            }},
            {"foodAmount", 1},
        }}
    };


    public static List<int> queue = Enumerable.Range(1, customers.Count).ToList();
    public static int[] customerQueue = ShuffleArray(queue);
    public static int queuePos = 0;

    void Start()
    {
        if (DataManager.GetDay() == 0)
        {
            queuePos = 0;
        }
    }

    public static int GetCustomerQueue()
    {
        return customerQueue[queuePos++];
    }

    public static int[] ShuffleArray(List<int> array)
    {
        System.Random random = new();
        return array.OrderBy(x => random.Next()).ToArray();
    }
}
