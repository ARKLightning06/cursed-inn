using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum NPCName {TestNPC, PohnJork, SirBorro, Gastou, OctaviousTheFifth, MayorThadworn, Wyl, Harry, Lupal}

public class NPCStats : MonoBehaviour
{
    [Header("Interactions")]
    public Player player;
    public UIManager uiManager;
    public InventoryManager inventoryManager;
    public Animator animator;

    [Header("Stats")]
    public NPCName character; // this is the character corresponding to this script, used for initializing dialogue stuff
    public string npcName;
    public int health;
    public bool isFighter;
    public int damage;
    public bool hasAnimation;
    public GameObject associatedItem;
    // attack type or smth somehow needs to go here...

    [Header("Dialogue")]
    public Sprite charVisualization;
    public Sprite playerVisualization;
    public string playerName;
    public Dialogue defaultDialogue;
    //public List<DialogueList> dialogueLists = new List<DialogueList>();
    public DialogueHandler handler;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // defaultDialogue = new Dialogue(defaultDialogue, "Default Dialogue", "Hello there!", true, true);
        // DialogueList tempStarter = new DialogueList();
        // tempStarter.dialogueOptions.Add(defaultDialogue);
        // handler = new DialogueHandler(defaultDialogue, dialogueLists, uiManager, charVisualization, "Nobody");
        animator = this.gameObject.GetComponent<Animator>();
        if(character != NPCName.TestNPC) // change back to == later!!!
        {
            //outline: first node is NPC saying Hello There! second node is Player saying General Kenobi, third node is NPC saying what's 9 + 10, fourth node is Player saying 19 or 21, 21 goes to NPC node saying wrong you uncultured swine try again, then back to fourth node, 21 is NPC saying good job, bye, then ending
            DialogueNode n1 = new DialogueNode(new List<Dialogue>(), false); // (NPC) hello there
            DialogueNode n2 = new DialogueNode(new List<Dialogue>(), true); // (Player) General Kenobi
            DialogueNode n3 = new DialogueNode(new List<Dialogue>(), false); // (NPC) What's 9 + 10? (options: 19, 21, in NEXT NODE)
            DialogueNode n4 = new DialogueNode(new List<Dialogue>(), true); // (Player) options: 19 or 21, 19 to n5, 21 to n6
            DialogueNode n5 = new DialogueNode(new List<Dialogue>(), false); // (NPC) 19 clicked, no you uncultured swine, try again, back to n3
            DialogueNode n6 = new DialogueNode(new List<Dialogue>(), false); // (NPC) 21 clicked, Good boyyyy. Good bye!
            DialogueNode end = new DialogueNode(new List<Dialogue>(), false); // empty node, end node to end dialogue 

            Dialogue d1 = new Dialogue(n2, "Hello There!", true); // stored in n1
            Dialogue d2 = new Dialogue(n3, "General Kenobi", true); // stored in n2
            Dialogue d3 = new Dialogue(n4, "What's 9 + 10?", true); // stored in n3
            Dialogue d4 = new Dialogue(n5, "19", true); // stored in n4
            Dialogue d5 = new Dialogue(n6, "21", true); // also stored in n4
            Dialogue d6 = new Dialogue(n3, "No you uncultured swine! Try again.", true); // stored in n5
            Dialogue d7 = new Dialogue(end, "Good boyyyy. Good bye!", true); // stored in n6

            n1.AppendDialogue(d1);
            n2.AppendDialogue(d2);
            n3.AppendDialogue(d3);
            n4.AppendDialogue(d4);
            n4.AppendDialogue(d5);
            n5.AppendDialogue(d6);
            n6.AppendDialogue(d7);

            handler = new DialogueHandler(n1, uiManager, npcName, charVisualization, playerName, playerVisualization);
        }
        else if(character == NPCName.SirBorro)
        {
            DialogueNode n1 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Good morrow! It is I, the Honorable Sir Borro, pleasure to make your acquintance fine sir, or lass. And who might I have the honor of speaking to? >n2
            DialogueNode n2 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Your name, >n3 2) ur mom, >n4 3) Sir Borro the II >n5 4) *spit in his food and leave* >n6 ***starts unavailable***
            DialogueNode n3 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Ah, *your name*, well met! Pray thee, share thy tale. Tell me about yourself. >n7
            DialogueNode n4 = new DialogueNode(new List<Dialogue>(), false); // (Borro) You scurvy knave! You impudent rapscallion! You villanous wretch! Fie, fie to thee and thine kin, and may the Lord above have mercy on thine own mother who had the misfortune of siring you >end ***malice +2****
            DialogueNode n5 = new DialogueNode(new List<Dialogue>(), false); // (Borro) A noble name! Surely you must be a noble warrior, like myself. Prithee, speak thy tale. Who art thou? >n7
            DialogueNode n6 = new DialogueNode(new List<Dialogue>(), false); // (Borro) How darest thou, insolent wretch! Thou art a cur, a scoundrel, a knave through and through! Have at thee! >n12
            DialogueNode n7 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) I am but a humble innkeeper, owner of this fine establishment. I think I might be cursed. >n8 2) I am emperor of all the world, and you are my willing servant. I command thee to aid me with my ailment, some fell curse. >n9 3) I'm a mass murder. I revel in slaughter and the blood of innocents. >n10 4) I'm no one of consequence. What about you, what's your story? >n11
            DialogueNode n8 = new DialogueNode(new List<Dialogue>(), false); // (Borro) A curse, eh? Fascinating. Who cursed you? Why would anyone do such a thing to such a fine fellow? What kind of curse is it? Whoever it was must hate you. It could be anyone in this very inn, could be someone watching you right here, right now... well, once you expose this evil sorcerer, tell me and we shall vanquish their dark magic forever, together! >n13
            DialogueNode n9 = new DialogueNode(new List<Dialogue>(), false); // (Borro)
            DialogueNode n10 = new DialogueNode(new List<Dialogue>(), false); // (Borro)
            DialogueNode n11 = new DialogueNode(new List<Dialogue>(), false); // (Borro)
            DialogueNode n12 = new DialogueNode(new List<Dialogue>(), false); // (Borro) *** Sir Borro throws his food at you. Gain Beef Stew *** >end
            DialogueNode n13 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Thank you, good knight. I thank you for your service, and look forward to fighting that evil together when the hour comes. >n14 2) If it could be anyone in this inn, how do I know it wasn't you?? >n15
            DialogueNode n14 = new DialogueNode(new List<Dialogue>(), false); // (Borro)
            DialogueNode n15 = new DialogueNode(new List<Dialogue>(), false); // (Borro)
            DialogueNode end = new DialogueNode(new List<Dialogue>(), false); // empty node, end node to end dialogue 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasAnimation)
        {
            if(player.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                animator.SetBool("PlayerToLeft", true);
                animator.SetBool("PlayerToRight", false);
            }
            else
            {
                animator.SetBool("PlayerToLeft", false);
                animator.SetBool("PlayerToRight", true);
            }
        }
    }
    
    public void DisplayDialogue()
    {
        handler.DisplayOptions();
    }
    
    public void DoNextDialogue(int option)
    {
        //option must be in range of Dialogue options, or else returns and turns off dialouge and resets currNode
        handler.GoToNextNode(option);
    }

    // Dialogue Option class to contain dialogue options

}


//     [System.Serializable]
//     public class Dialogue
//     {
//         public Dialogue nextDialogue;
//         public bool isPlayerDialogue;
//         public bool isLast; // true if this is the last dialogue before sending them on their way
//         public string messageName;
//         public string message;
//         public bool originalIsAvailableMessage;
//         public bool isAvailableMessage;

//         // PLAYER STATS ONLY 

//         // Potential consequences for selecting this dialogue option (by default 0)
//         public int selectedMalice;
//         public int selectedHonor;
//         public int selectedKindness;
//         //add more specific quests, like selectedNecromancer or something, as needed

//         //Default constructor: for NPC dialogue only, not player dialogue!
// //         public Dialogue(Dialogue next, string mName, string mMessage, bool startsAvailable, bool last)
//         {
//             nextDialogue = next;
//             isLast = last;
//             isPlayerDialogue = false;
//             messageName = mName;
//             message = mMessage;
//             originalIsAvailableMessage = startsAvailable;
//             isAvailableMessage = startsAvailable;
//             selectedMalice = 0;
//             selectedHonor = 0;
//             selectedKindness = 0;
//         }

//         //Player dialogue option constructor only!
//         public Dialogue(Dialogue next, string mName, string mMessage, bool startsAvailable, int malice, int honor, int kindness, bool last)
//         {
//             nextDialogue = next;
//             isLast = last;
//             isPlayerDialogue = false;
//             messageName = mName;
//             message = mMessage;
//             originalIsAvailableMessage = startsAvailable;
//             isAvailableMessage = startsAvailable;
//             selectedMalice = malice;
//             selectedHonor = honor;
//             selectedKindness = kindness;
//         }

//         public void ToggleAvailable(bool isAvailable)
//         {
//             isAvailableMessage = isAvailable;
//         }

//     public string GetMessage()
//     {
//         return message;
//     }

//     public bool IsLast()
//     {
//         return isLast;
//     }

//     public Dialogue GetNext()
//     {
//         return nextDialogue;
//     }
        
//     }

//     // gonna use bools instead of subclasses ig because SerializeReference isn't working even after 2 hours of GPTing, oh well

//     // [System.Serializable]
//     // public class PlayerDialogue : Dialogue
//     // {
//     //     public int test;
//     // }

//     // [System.Serializable]
//     // public class NPCDialogue : Dialogue
//     // {
//     //     public int testing;
//     // }


//     // Dialogue List
//     [System.Serializable]
//     public class DialogueList
//     {
//         public string listName; // name of this set of dialogue options, e.g. 'base dialogue' or 'angry dialogue' etc
//         public List<Dialogue> dialogueOptions = new List<Dialogue>();
//         public bool isAvailableList;

//         public void ToggleAvailability(bool isAvailable)
//         {
//             foreach(Dialogue dialogue in dialogueOptions)
//             {
//                 dialogue.ToggleAvailable(isAvailable);
//             }
//             isAvailableList = isAvailable;
//         }

//     public Dialogue GetFirstDialogue()
//     {
//         return dialogueOptions[0];
//     }
        
//     }
// [System.Serializable]
// public class DialogueHandler
// {
//     public Dialogue currDialogue;
//     public List<DialogueList> allDialogueLists;
//     public DialogueList currDialogueList;
//     public UIManager uiManager;
//     public Sprite charVis;
//     public string charName;

//     public DialogueHandler(Dialogue curr, List<DialogueList> currList, UIManager ui, Sprite npc, string charName)
//     {
//         currDialogue = curr;
//         allDialogueLists = currList;
//         currDialogueList = currList[0];
//         uiManager = ui;
//         charVis = npc;
//     }

//     public void NextDialogueOption()
//     {
//         if(!currDialogue.IsLast())
//         {
//             Debug.Log("Talking!!");
//             Debug.Log("Handler thinks the message is: " + currDialogue.GetMessage());
//             uiManager.UpdateDialogue(charVis, charName, currDialogue.GetMessage());
//             currDialogue = currDialogue.GetNext();
//         }
//         else
//         {
//             uiManager.TurnOffDialogue();
//             currDialogue = currDialogueList.GetFirstDialogue();
//         }
//     }
// }





// TAKE TWO LMAO
// STRUCTURE: DIALOGUEHANDLER IS A LINKEDLIST FOR DIALOGUELIST NODES, EACH NODE HAS A LIST OF DIALOGUE OPTIONS TO CHOOSE FROM WHICH LINK TO THE NEXT DIALOGUELIST ITEM
// SO BASICALLY THE _NEXT ATTRIBUTE IS STORED IN A LIST OF DIFFERENT POSSIBILITIES CALLED DIALOGUES WHICH ALSO HAVE BOOLS AND MESSAGES AND STUFF

[System.Serializable]
public class DialogueHandler
{
    // LinkedList of different DialogueNode nodes, where each DialogueNode class is a node of LinkedList and the next node is stored in a list of Dialogue class options
    // Attributes: 
    // - originalNode: contains original node class that is the start of the conversation essentially
    // - currNode: current Dialogue node the conversation is on
    // - uiManager: uiManager object to update dialogue boxes with
    // - charName: name of character
    // - charVis: visualization sprite for the character
    // Functions:
    // - DisplayOptions(): works with UIManager to display all dialogue options and add buttons next to each one 
    // - GoToNextNode(int): calls currNode's GetNextNode() function and updates currNode = the new node
    // - ResetHandler(): resets currNode to originalNode and updates all DialogueNodes
    // - SetOriginalNode(): sets originalNode to a new node
    // - SetCurrNode(): sets currNode to a new node

    public DialogueNode originalNode;
    public DialogueNode currNode;
    public UIManager uiManager;
    public string charName;
    public Sprite charVis;
    public string playerName;
    public Sprite playerVis;
    
    public DialogueHandler(DialogueNode ogNode, UIManager ui, string cName, Sprite cVis, string pName, Sprite pVis)
    {
        originalNode = ogNode;
        currNode = ogNode;
        uiManager = ui;
        charName = cName;
        charVis = cVis;
        playerName = pName;
        playerVis = pVis;
    }

    public void DisplayOptions()
    {
        //uimanager shenanigans...?
        if(!currNode.IsCurrentlyEmpty())
        {
            currNode.UpdateAvailability();
            if(currNode.IsPlayerNode())
            {
                // if(GameObject.Find("SaveData") != null)
                // {
                //     // playerName = GameObject.Find("SaveData").GetComponent<SaveData>().GetName();
                // }
                playerName = SaveData.saveData.GetName();
                uiManager.UpdateDialogue(playerVis, playerName, currNode.GetDialogueOptions(), currNode.GetNumOptions(), true);
            }
            else
            {
                uiManager.UpdateDialogue(charVis, charName, currNode.GetDialogueOptions(), currNode.GetNumOptions(), false);   
            }
        }
        else
        {
            uiManager.TurnOffDialogue();
            currNode = originalNode;
            Debug.Log("Empty Node, Dialogue finished");
        }
    }

    public void GoToNextNode(int chosen)
    {
        //chosen must be in range of Dialogue options, or else returns and turns off dialouge and resets currNode
        if(!currNode.IsCurrentlyEmpty() && currNode.GetNumOptions() >= chosen)
        {
            currNode.GetNextDialogue(chosen).SelectedResults();
            currNode = currNode.GetNextDialogue(chosen).GetNextNode();
            DisplayOptions();
        }
        else
        {
            uiManager.TurnOffDialogue();
            currNode = originalNode;
            Debug.Log("Empty Node, Dialogue finished");

        }        
        
    }
    
}

[System.Serializable]
public class DialogueNode
{
    // Node of the DialogueHandler LinkedList containing a list of at least one dialogue option(s)
    // Attributes:
    // - DialogueList: list of Dialogue options to choose from which will also correspond to next node essentially
    // - isPlayerDialogue: boolean true if this node contains player dialogue or false if contains NPC dialogue
    // - DISREGARD: nextDefault: the default next DialogueOption used for searching through a Handler or something
    // Functions:
    // - GetNextDialogue(int): returns the chosen Dialogue option
    // - GetDialogueOptions(): returns list of dialogue messages
    // - GetNumOptions(): returns integer number of options, should be 0-4 (0 is empty, 1 is a list with only index 0)
    // - UpdateAvailability: updates Availability of all Dialogue children under this node
    // - IsCurrentlyEmpty(): returns true if current dialogueList attribute is empty or has no currently available options, false if there exists at least one Available Dialogue in dialogueList
    // - IsPlayerNode(): returns isPlayerDialogue bool
    // - AppendDialogue(Dialogue): appends dialogue to the end of dialogueList
    public List<Dialogue> dialogueList = new List<Dialogue>();
    // public Dialogue nextDefault;
    public bool isPlayerDialogue;
    

    public DialogueNode(List<Dialogue> dList, bool isPlay)
    {
        dialogueList = dList;
        // nextDefault = nextD; DISREGARD but if change mind don't forget to edit constructor parameters 
        isPlayerDialogue = isPlay;
    }

    public Dialogue GetNextDialogue(int chosen)
    {
        //chosen must be less than or equal to number of options (which should be at most four)
        int index = 1;
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                if(index == chosen)
                {
                    return d;
                }
                index += 1;
            }
        }
        Debug.Log("SOMETHING WENT WRONG NPCSTATS DIALOGUENODE GETNEXTDIALOGUE() SHOULD NOT REACH THIS POINT, ERROR WITH DIALOUGE SETUP MAYBE? OR THIS IS IMPLEMENTED WRONG AND FIX IT");
        return dialogueList[0];

    }

    public string GetDialogueOptions()
    {
        string options = "";
        // only called if dialogueList is not empty
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                options += d.GetMessage();
                options += "\n";
            }
        }
        return options;
    }

    public int GetNumOptions()
    {
        int numOptions = 0;
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                numOptions += 1;
            }
        }
        return numOptions;
    }

    public void ResetAvailability()
    {
        foreach(Dialogue d in dialogueList)
        {
            d.SetAvailability(d.startAvailable, d.startAvailable);
        }
    }

    public void UpdateAvailability()
    {
        foreach(Dialogue d in dialogueList)
        {
            d.CheckAvailabilityUpdate();
        }
    }

    public bool IsCurrentlyEmpty()
    {
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                return false;
            }
        }
        return true;
    }

    public bool IsPlayerNode()
    {
        return isPlayerDialogue;
    }

    public void AppendDialogue(Dialogue d)
    {
        // this probably doesn't work...
        dialogueList.Add(d);
    }
}

[System.Serializable]
public class Dialogue
{
    // A class representing a single dialogue option, which may be either an NPC dialogue option or a character dialogue option
    // Attributes:
    // - nextNode: DialogueNode class containing the next linked DialogueNode node
    // - message: string with the message to be displayed 
    // - startAvailable: boolean true if starts available, false if starts unavailable
    // - currentlyAvailable: boolean true if currently available, false if not
    // - maliceResult: integer by default 0 representing any added malice from choosing this option
    // - honorResult: integer by default 0 representing any added honor from choosing this option
    // - kindnessResult: integer by default 0 representing any added kindness for choosing this option 
    // Functions
    // - UpdateAvailability: updates currentlyAvailable and/or startAvailable based off of various parameters 
    // - GetAvailability: returns true if currentlyAvailable and false otherwise
    // - GetMessage(): returns string of the message of this Dialogue option
    // - SelectedResults(): updates malice, honor, and kindness in QuestManager, as well as any other consequences that arise
    // - GetNextNode(): returns nextNode object

    // Attributes
    public DialogueNode nextNode;
    public string message;
    public bool startAvailable;
    public bool currentlyAvailable;
    public int maliceResult;
    public int honorResult;
    public int kindnessResult;
    public string doSomething; // "Do nothing" by default, set to something else if selecting this dialogue does something important
    public string availabilityState; // "Default" by default, set to something else if the availability of the Dialogue option depends on some condition (like malice honor kindness etc)
    public NPCStats parentNPC; // null by default, only used for certain dialogue options

    // Constructors 
    public Dialogue(DialogueNode dNode, string m, bool startA, int malice, int honor, int kindness, string actionToDo, string availabilityCondition, NPCStats parent)
    {
        nextNode = dNode;
        message = m;
        startAvailable = startA;
        currentlyAvailable = startA;
        maliceResult = malice;
        honorResult = honor;
        kindnessResult = kindness;
        doSomething = actionToDo;
        availabilityState = availabilityCondition;
        parentNPC = parent;
    }
    
    public Dialogue(DialogueNode dNode, string m, bool startA)
    {
        nextNode = dNode;
        message = m;
        startAvailable = startA;
        currentlyAvailable = startA;
        maliceResult = 0;
        honorResult = 0;
        kindnessResult = 0;
        doSomething = "Do nothing";
        availabilityState = "Default"; 
    }

    // Methods
    public void SetAvailability(bool setStart, bool setCurr)
    {
        // if(toggleStart)
        // {
        //     startAvailable = !startAvailable;
        // }
        // if(toggleCurr)
        // {
        //     currentlyAvailable = !currentlyAvailable;
        // }
        startAvailable = setStart;
        currentlyAvailable = setCurr;
    }

    public void CheckAvailabilityUpdate()
    {
        if(availabilityState == "Default")
        {
            Debug.Log("available by default :)");
        }
        else if(availabilityState == "Slightly mean")
        {
            SetAvailability(startAvailable, SaveData.saveData.GetMalice() >= 5);
        }
    }

    public bool GetAvailability()
    {
        return currentlyAvailable;
    }

    public string GetMessage()
    {
        return message;
    }

    public void SelectedResults()
    {
        // idk smth with questManager that doesn't exist yet and malice honor kindness
        if(doSomething == "Do nothing")
        {
            Debug.Log("Nothing to do");
        }
        else if(doSomething == "Turn this option off temporarily")
        {
            SetAvailability(startAvailable, false);
        }
        else if(doSomething == "Add Item")
        {
            parentNPC.inventoryManager.AddItemToInventory(parentNPC.associatedItem);
            SaveData.saveData.AddItemToStarters(parentNPC.associatedItem);
        }
    }

    public DialogueNode GetNextNode()
    {
        return nextNode;
    }
}
