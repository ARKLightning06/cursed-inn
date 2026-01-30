using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SaveData : MonoBehaviour
{
    [Header("Object assignments")]
    //Singleton
    public static SaveData saveData;
    public ManagerOfScenes sceneM;
    [Header("Attributes to keep between time loops")]
    public string playerName;
    public List<GameObject> savedStarterItems = new List<GameObject>();
    // This should be in it's own Questmanager script or smth but not enough time to do all that, just using singleton is easier sorry for bad structure can fix later maybe (lol)
    [Header("Publically accessible counts because lazy")]
    public int maliceCount;
    public int honorCount;
    public int kindnessCount;

    void Awake()
    {
        if(saveData != null && saveData != this)
        {
            Destroy(this);
        }
        else
        {
            saveData = this;
        }

        playerName = "You";
        DontDestroyOnLoad(gameObject);
    }

    public string GetName()
    {
        return playerName;
    }

    public void UpdateName(string newName)
    {
        playerName = newName;
    }

    public List<GameObject> GetStarterItems()
    {
        return savedStarterItems;
    }

    public int GetMalice()
    {
        return maliceCount;
    }
    
    public int GetHonor()
    {
        return honorCount;
    }

    public int GetKindness()
    {
        return kindnessCount;
    }

    public void AddMalice(int addedMalice)
    {
        maliceCount += addedMalice;
    }

    public void AddHonor(int addedHonor)
    {
        honorCount += addedHonor;
    }

    public void AddKindness(int addedKindness)
    {
        kindnessCount += addedKindness;
    }

    public void AddItemToStarters(GameObject addedItem)
    {
        if(!savedStarterItems.Contains(addedItem))
        {
            savedStarterItems.Add(addedItem);
        }
    }

    public void ResetTimeLoop()
    {
        maliceCount = 0;
        honorCount = 0;
        kindnessCount = 0;
        sceneM.ReloadScene();
    }
}
