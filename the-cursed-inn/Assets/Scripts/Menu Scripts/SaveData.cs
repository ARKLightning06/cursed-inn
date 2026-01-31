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
    public float timerLength;
    public float timeRemaining;
    private float minutes;
    private float seconds;
    public bool timeIsRunning;
    [Header("Attributes to keep between time loops")]
    public string playerName;
    public List<GameObject> savedStarterItems = new List<GameObject>();
    public GameObject bottle;
    // This should be in it's own Questmanager script or smth but not enough time to do all that, just using singleton is easier sorry for bad structure can fix later maybe (lol)
    [Header("Publically accessible counts because lazy")]
    public int maliceCount;
    public int honorCount;
    public int kindnessCount;
    [Header("Publically accessible booleans because lazy")]
    public bool knowsAllergy;
    // Borro = 0, Gastou = 1, Pohn = 2, Wyl = 3, Octavia = 4, Lupal = 5, Thadworn = 6 Harry = 7
    public bool[] metPeople = {false, false, false, false, false, false, false, false};
    public bool[] knowsPeoplesSecrets = {false, false, false, false, false, false, false, false};

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
        timeRemaining = timerLength;
        
    }

    void Update()
    {
        if(timeIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                minutes = Mathf.FloorToInt(timeRemaining / 60);
                seconds = Mathf.FloorToInt(timeRemaining % 60);
            }
            else
            {
                timeRemaining = 0;
                minutes = 0;
                seconds = 0;
                // timeIsRunning = false;
                ResetTimeLoop();
            }
        }
    }

    public bool GetTimerRunning()
    {
        return timeIsRunning;
    }

    public float GetMinutes()
    {
        return minutes;
    }
    
    public float GetSeconds()
    {
        return seconds;
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

    public bool GetKnowsAllergy()
    {
        return knowsAllergy;
    }

    public void SetKnowsAllergy(bool knows)
    {
        knowsAllergy = knows;
    }

    public bool GetHasMeat()
    {
        foreach(GameObject item in savedStarterItems)
        {
            if(item.GetComponent<ItemStats>().itemName == "Beef Stew") // check what the actual name is!
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateHasMetNPC(int id)
    {
        // Borro = 0, Gastou = 1, Pohn = 2, Wyl = 3, Octavia = 4, Lupal = 5, Thadworn = 6 Harry = 7
        metPeople[id] = true;
    }

    public bool GetHasMet(int id)
    {
        return metPeople[id];
    }

    public void UpdateKnowsNPCSecret(int id)
    {
        knowsPeoplesSecrets[id] = true;
    }

    public bool GetKnowsNPCSecret(int id)
    {
        return knowsPeoplesSecrets[id];
    }


    public void ResetTimeLoop()
    {
        maliceCount = 0;
        honorCount = 0;
        kindnessCount = 0;
        timeRemaining = timerLength;
        sceneM.ReloadScene();
    }

    public void ResetAll()
    {
        for(int i = 0; i < 8; i++)
        {
            metPeople[i] = false;
            knowsPeoplesSecrets[i] = false;
            maliceCount = 0; 
            honorCount = 0;
            kindnessCount = 0;
            timeRemaining = timerLength;
            savedStarterItems.Clear();
            savedStarterItems.Add(bottle);
            knowsAllergy = false;
        }
    }
}
