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
    public List<DialogueList> dialogueLists = new List<DialogueList>();
    public DialogueHandler handler;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultDialogue = new Dialogue(defaultDialogue, "Default Dialogue", "Hello there!", true, true);
        DialogueList tempStarter = new DialogueList();
        tempStarter.dialogueOptions.Add(defaultDialogue);
        handler = new DialogueHandler(defaultDialogue, dialogueLists, uiManager, charVisualization, "Nobody");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DoNextDialogue()
    {
        handler.NextDialogueOption();
    }

    // Dialogue Option class to contain dialogue options

}


    [System.Serializable]
    public class Dialogue
    {
        public Dialogue nextDialogue;
        public bool isPlayerDialogue;
        public bool isLast; // true if this is the last dialogue before sending them on their way
        public string messageName;
        public string message;
        public bool originalIsAvailableMessage;
        public bool isAvailableMessage;

        // PLAYER STATS ONLY 

        // Potential consequences for selecting this dialogue option (by default 0)
        public int selectedMalice;
        public int selectedHonor;
        public int selectedKindness;
        //add more specific quests, like selectedNecromancer or something, as needed

        //Default constructor: for NPC dialogue only, not player dialogue!
        public Dialogue(Dialogue next, string mName, string mMessage, bool startsAvailable, bool last)
        {
            nextDialogue = next;
            isLast = last;
            isPlayerDialogue = false;
            messageName = mName;
            message = mMessage;
            originalIsAvailableMessage = startsAvailable;
            isAvailableMessage = startsAvailable;
            selectedMalice = 0;
            selectedHonor = 0;
            selectedKindness = 0;
        }

        //Player dialogue option constructor only!
        public Dialogue(Dialogue next, string mName, string mMessage, bool startsAvailable, int malice, int honor, int kindness, bool last)
        {
            nextDialogue = next;
            isLast = last;
            isPlayerDialogue = false;
            messageName = mName;
            message = mMessage;
            originalIsAvailableMessage = startsAvailable;
            isAvailableMessage = startsAvailable;
            selectedMalice = malice;
            selectedHonor = honor;
            selectedKindness = kindness;
        }

        public void ToggleAvailable(bool isAvailable)
        {
            isAvailableMessage = isAvailable;
        }

    public string GetMessage()
    {
        return message;
    }

    public bool IsLast()
    {
        return isLast;
    }

    public Dialogue GetNext()
    {
        return nextDialogue;
    }
        
    }

    // gonna use bools instead of subclasses ig because SerializeReference isn't working even after 2 hours of GPTing, oh well

    // [System.Serializable]
    // public class PlayerDialogue : Dialogue
    // {
    //     public int test;
    // }

    // [System.Serializable]
    // public class NPCDialogue : Dialogue
    // {
    //     public int testing;
    // }


    // Dialogue List
    [System.Serializable]
    public class DialogueList
    {
        public string listName; // name of this set of dialogue options, e.g. 'base dialogue' or 'angry dialogue' etc
        public List<Dialogue> dialogueOptions = new List<Dialogue>();
        public bool isAvailableList;

        public void ToggleAvailability(bool isAvailable)
        {
            foreach(Dialogue dialogue in dialogueOptions)
            {
                dialogue.ToggleAvailable(isAvailable);
            }
            isAvailableList = isAvailable;
        }

    public Dialogue GetFirstDialogue()
    {
        return dialogueOptions[0];
    }
        
    }
[System.Serializable]
public class DialogueHandler
{
    public Dialogue currDialogue;
    public List<DialogueList> allDialogueLists;
    public DialogueList currDialogueList;
    public UIManager uiManager;
    public Sprite charVis;
    public string charName;

    public DialogueHandler(Dialogue curr, List<DialogueList> currList, UIManager ui, Sprite npc, string charName)
    {
        currDialogue = curr;
        allDialogueLists = currList;
        currDialogueList = currList[0];
        uiManager = ui;
        charVis = npc;
    }

    public void NextDialogueOption()
    {
        if(!currDialogue.IsLast())
        {
            uiManager.UpdateDialogue(charVis, charName, currDialogue.GetMessage());
            currDialogue = currDialogue.GetNext();
        }
        else
        {
            uiManager.TurnOffDialogue();
            currDialogue = currDialogueList.GetFirstDialogue();
        }
    }
}
