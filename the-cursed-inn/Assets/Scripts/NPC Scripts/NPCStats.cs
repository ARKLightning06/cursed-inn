using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class NPCStats : MonoBehaviour
{
    [Header("Interactions")]
    public Player player;
    public UIManager uiManager;

    [Header("Stats")]
    public string npcName;
    public int health;
    public bool isFighter;
    public int damage;
    // attack type or smth somehow needs to go here...

    [Header("Dialogue")]
    public Sprite charVisualization;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DoNextDialogue()
    {
        handler.GoToNextNode();
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
    
    public DialogueHandler(DialogueNode ogNode, UIManager ui, string cName, Sprite cVis)
    {
        originalNode = ogNode;
        currNode = ogNode;
        uiManager = ui;
        charName = cName;
        charVis = cVis;
    }

    public void DisplayOptions()
    {
        //uimanager shenanigans...?
    }

    public void GoToNextNode()
    {
        Debug.Log("Under construction lmao, gotta lock in for frisbee but smth with caling currNode.GetNext and setting cur to that or smth");
    }
    
}

public class DialogueNode
{
    // Node of the DialogueHandler LinkedList containing a list of at least one dialogue option(s)
    // Attributes:
    // - DialogueList: list of Dialogue options to choose from which will also correspond to next node essentially
    // - nextDefault: the default next DialogueOption used for searching through a Handler or something
    // Functions:
    // - GetNextNode(int): returns the nextNode based off of chosen DialogueOption
    // - GetDialogueOptions: returns list of dialogue messages
    // - UpdateAvailability: updates Availability of all Dialogue children under this node
}

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
}
