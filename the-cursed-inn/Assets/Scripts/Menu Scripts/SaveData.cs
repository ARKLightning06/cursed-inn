using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SaveData : MonoBehaviour
{
    //Singleton
    public static SaveData saveData;
    // Script to save any data that needs to exists between scenes etc
    public string playerName;
    public List<GameObject> savedStarterItems = new List<GameObject>();

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

}
