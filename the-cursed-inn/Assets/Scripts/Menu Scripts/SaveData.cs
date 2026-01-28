using UnityEngine;

public class SaveData : MonoBehaviour
{
    //Singleton
    public static SaveData saveData;
    // Script to save any data that needs to exists between scenes etc
    public string playerName;

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

}
