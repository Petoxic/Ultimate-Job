using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class CustomerData : MonoBehaviour
{
    // constant data for customers
    private static readonly Dictionary<int, Dictionary<string, object>> customersDay1 = new()
    {
        {
            1, new Dictionary<string, object> {
            {"name", "Mrs. Evelyn Sinclair - Rich lady"},
            {"dialogue", new string[][] {
                new string[] {
                    "Oh my, did you hear? Someone swiped a necklace from the museum. Horrid stuff. Who'd do such a thing? It's just awful!"
                }
            }},
            {"foodAmount", 1},
        }},
        {
            2, new Dictionary<string, object> {
            {"name", "Mr. Daniel Blackwood - Art fan"},
            {"dialogue", new string[][] {
                new string[] {
                    "Art thefts, you say? Fascinating! Reminds me of that daring heist in Paris back in '98.",
                    "Wish I'd been there! What a story that would've been!"
                },
            }},
            {"foodAmount", 1},
        }},
        {
            3, new Dictionary<string, object> {
            {"name", "Officer Jameson - Local cop"},
            {"dialogue", new string[][] {
                new string[] {
                    "Another day, another crime scene. This city's getting worse by the minute.",
                    "You'd think with all the security measures in place, we'd catch these criminals red-handed. But no luck so far."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            4, new Dictionary<string, object> {
            {"name", "Mrs. Margaret Chen - Museum boss"},
            {"dialogue", new string[][] {
                new string[] {
                    "The theft has shaken us all. That necklace was the star of our show.",
                    "We've implemented strict security protocols, but it seems someone slipped through the cracks."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            5, new Dictionary<string, object> {
            {"name", "Mr. Oliver Ford - Suspicious-looking man"},
            {"dialogue", new string[][] {
                new string[] {
                    "Me? Oh, I'm just here for the food. Don't mind me. Say, you wouldn't happen to be looking for someone, would you? Maybe I can help.",
                },
            }},
            {"foodAmount", 1},
        }},
        {
            6, new Dictionary<string, object> {
            {"name", "Ms. Lily Thompson - Aspiring artist"},
            {"dialogue", new string[][] {
                new string[] {
                    "I can't believe someone would steal from a museum. It's shameful! Those pieces belong to everyone, not just the rich and powerful.",
                    "If I ever caught the thief, they'd hear about it!"
                },
            }},
            {"foodAmount", 1},
        }},
        {
            7, new Dictionary<string, object> {
            {"name", "Dr. Henry Wallace - Science dude"},
            {"dialogue", new string[][] {
                new string[] {
                    "Crime scene was like solving a puzzle. Every bit matters.",
                    "But even with our gadgets, we're lost. It's tough."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            8, new Dictionary<string, object> {
            {"name", "Mrs. Beatrice Ramirez - Local gossip"},
            {"dialogue", new string[][] {
                new string[] {
                    "Oh, honey, you wouldn't believe the rumors I've heard about that museum theft.",
                    "They say it was an inside job! Can you imagine? Someone from the staff betraying their own workplace? Scandalous, I tell you!"
                },
            }},
            {"foodAmount", 1},
        }},
        {
            9, new Dictionary<string, object> {
            {"name", "Mr. Charles Thompson - Retired detective"},
            {"dialogue", new string[][] {
                new string[] {
                    "I've seen it all, but this one's tricky.",
                    "Criminals slip up, though. Just gotta wait and watch. We'll get 'em."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            10, new Dictionary<string, object> {
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
    public static Dictionary<int, Dictionary<string, object>> customersDay2 = new()
    {
        {
            1, new Dictionary<string, object> {
            {"name", "Mr. Richard Banks - Wealthy businessman"},
            {"dialogue", new string[][] {
                new string[] {
                    "Have you heard about the bank robbery? Simply outrageous! Who could have the audacity to pull off such a stunt? It's scandalous!"
                }
            }},
            {"foodAmount", 1},
        }},
        {
            2, new Dictionary<string, object> {
            {"name", "Ms. Victoria Artman - Art enthusiast"},
            {"dialogue", new string[][] {
                new string[] {
                    "Bank heists? Intriguing! Reminds me of those old Hollywood movies. If only I could witness one in real life! What a thrill it would be!"
                },
            }},
            {"foodAmount", 1},
        }},
        {
            3, new Dictionary<string, object> {
            {"name", "Officer Johnson - Local law enforcement"},
            {"dialogue", new string[][] {
                new string[] {
                    "Another day, another robbery. This city's turning into a den of thieves. We've got eyes everywhere, but these crooks always slip through our fingers."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            4, new Dictionary<string, object> {
            {"name", "Mr. David Chase - Bank manager"},
            {"dialogue", new string[][] {
                new string[] {
                    "The robbery has shaken us to the core. Security measures were in place, but it seems the thieves were one step ahead. We're doing everything we can to catch them."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            5, new Dictionary<string, object> {
            {"name", "Ms. Olivia Fox - Suspicious individual"},
            {"dialogue", new string[][] {
                new string[] {
                    "Me? Just here for the coffee. Don't mind me. Say, need any help tracking down those robbers? I might know a thing or two."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            6, new Dictionary<string, object> {
            {"name", "Mr. Samuel Locke - Aspiring artist"},
            {"dialogue", new string[][] {
                new string[] {
                    "Robbing a bank? Disgraceful! Money belongs to everyone, not just the greedy. If I catch those thieves, they'll regret ever crossing me!"
                },
            }},
            {"foodAmount", 1},
        }},
        {
            7, new Dictionary<string, object> {
            {"name", "Dr. Emily Watson - Forensic scientist"},
            {"dialogue", new string[][] {
                new string[] {
                    "Analyzing the crime scene was like putting together a puzzle. Every clue counts, but even with our advanced technology, we're struggling to make sense of it all."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            8, new Dictionary<string, object> {
            {"name", "Mrs. Grace Parker - Local gossipmonger"},
            {"dialogue", new string[][] {
                new string[] {
                    "Oh, honey, the gossip about the bank robbery is wild! Some say it was an inside job! Can you believe it? A bank employee turned rogue? Outrageous!"
                },
            }},
            {"foodAmount", 1},
        }},
        {
            9, new Dictionary<string, object> {
            {"name", "Mr. Robert Holmes - Retired detective"},
            {"dialogue", new string[][] {
                new string[] {
                    "I've investigated my fair share of cases, but this one's got me stumped. However, every criminal slips up eventually. It's just a matter of time."
                },
            }},
            {"foodAmount", 1},
        }},
        {
            10, new Dictionary<string, object> {
            {"name", "Ms. Rachel Stone - Mystery enthusiast"},
            {"dialogue", new string[][] {
                new string[] {
                    "That robbery? More than meets the eye. It's not just about the money. There's a motive behind it all. But what could it be? That's the million-dollar question."
                },
            }},
            {"foodAmount", 1},
        }}
    };
    private static readonly Dictionary<int, Dictionary<string, object>>[] customers = new Dictionary<int, Dictionary<string, object>>[]
    {
        customersDay1,
        customersDay2
    };

    public static Dictionary<int, Dictionary<string, object>> GetCustomerData()
    {
        return customers[DataManager.GetCaseNumber()];
    }
}
