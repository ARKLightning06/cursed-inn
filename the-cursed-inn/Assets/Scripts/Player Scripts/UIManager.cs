using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

// GameState enum to track GameState
public enum GameState {Menu, Paused, Inventory, Playing, Cutscene}

// Class to manager UI functionality
// This includes pausing and unpausing the game, keeping track of buttons and other elements of UI, and adjusting setttings.
public class UIManager : MonoBehaviour
{
    private InputSystem_Actions controls;
    public InventoryManager inventoryManager;
    public GameState currentState;

    //UI Components
        //(Original idea was to have lists but just putting everything under parent objects is way easier and cleaner, this is outdated code but might switch back to it for some things later idk)
    // public List<Button> pauseButtons = new List<Button>();
    // public List<Button> playButtons = new List<Button>();
    // public List<Button> inventoryButtons = new List<Button>();
    // public List<Button> menuButtons = new List<Button>();
    // public List<Button> settingsButtons = new List<Button>();
    public List<GameObject> parentUIs = new List<GameObject>();
    public GameObject pauseUI;
    public GameObject playUI;
    public GameObject inventoryUI;
    public GameObject menuUI;
    public GameObject settingsUI;
    public GameObject hotbarUI;
    public GameObject dialogueUI;
    public GameObject timerUI;
    public GameObject journalUI;
    public TMP_Text timerText;
    public TMP_Text dialogueName;
    public TMP_Text dialogueText;
    public Image dialogueImage;
    public List<GameObject> dialogueButtons = new List<GameObject>();
    public GameObject nextButton;
    public Sprite fullHeartImage;
    public Sprite emptyHeartImage;
    public List<Image> heartList = new List<Image>();
    public Image maliceBar;
    public Image honorBar;
    public Image kindnessBar;
    public TMP_Text notesText;
    public TMP_Text relationshipText;
    public Slider maliceSlider;
    public Slider honorSlider;
    public Slider kindnessSlider;
    public float maxRelationshipValue;
    public string[] notesArray = {"- Cursed in infinite loop", "- Glass bottle: ???", "- ???: ???", "- ???: ???", "- ???: ???", "- ???: ???", "- ???: ???", "- ???: ???", "- ???: ???", "- ???: ???"};
    public float textSpeed;
    
    // and any others...

// Setting up Input
    private void Awake()
    {
        // Initialize the InputSystem_Actions() instance
        controls = new InputSystem_Actions();
        
        // Subscribe to the Inventory action
        controls.UI.Pause.performed += ctx => Pause();

        //Start on GameState Playing (for now)
        currentState = GameState.Playing;
        parentUIs.Add(pauseUI);
        parentUIs.Add(playUI);
        parentUIs.Add(inventoryUI);
        parentUIs.Add(menuUI);
        parentUIs.Add(settingsUI);
        parentUIs.Add(hotbarUI);
        parentUIs.Add(timerUI);
        parentUIs.Add(journalUI);
        TurnOnPlayingUI();
    }

    void Update()
    {
        if(SaveData.saveData.GetTimerRunning())
        {
            timerText.text = string.Format("{0:00}:{1:00}", SaveData.saveData.GetMinutes(), SaveData.saveData.GetSeconds());
        }
    }


    private void OnEnable()
    {
        // Enable the input controls
        controls.Enable();
    }

    private void OnDisable()
    {
        // Disable the input controls when the player object is disabled
        controls.Disable();
    }

//Playing

    public void SetHearts(int numHeartsTotal, int numHeartsAlive)
    {
        //numHeartsAlive <= numHeartsTotal <= heartList.Count - 1
        for (int i = 0; i < heartList.Count; i++)
        {
            if (i < numHeartsAlive)
            {
                heartList[i].gameObject.SetActive(true);
                heartList[i].sprite = fullHeartImage;
            }
            else if (i < numHeartsTotal)
            {
                heartList[i].gameObject.SetActive(true);
                heartList[i].sprite = emptyHeartImage;
            }
            else
            {
                heartList[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetMaliceBar(float percent)
    {
        maliceBar.fillAmount = percent;
    }

    public void SetHonorBar(float percent)
    {
        honorBar.fillAmount = percent;
    }

    public void SetKindnessBar(float percent)
    {
        kindnessBar.fillAmount = percent;
    }

//Pausing
    public void Pause()
    {
        if(currentState != GameState.Paused)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0f;
        TurnOnPauseMenuUI();
    }
    
    public void UnpauseGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        TurnOnPlayingUI();
    }


// Buttons and other UI

    public void TurnEverythingOff()
    {
        inventoryManager.ToggleSlots(false);
        foreach (GameObject go in parentUIs)
        {
            go.SetActive(false);
        }
        // everything else is irrelevant (outdated)
        // foreach (Button button in pauseButtons)
        // {
        //     button.gameObject.SetActive(false);
        // }
        // foreach (Button button in inventoryButtons)
        // {
        //     button.gameObject.SetActive(false);
        // }
        // foreach (Button button in playButtons)
        // {
        //     button.gameObject.SetActive(false);
        // }
        // foreach (Button button in menuButtons)
        // {
        //     button.gameObject.SetActive(false);
        // }

    }

    public void TurnOnInventoryUI()
    {
        TurnEverythingOff();
        timerUI.SetActive(true);
        inventoryUI.SetActive(true);
        hotbarUI.SetActive(true);
        inventoryManager.ToggleSlots(true);
    }

    public void TurnOnPauseMenuUI()
    {
        TurnEverythingOff();
        timerUI.SetActive(true);
        pauseUI.SetActive(true);
        //...
    }

    public void TurnOnPlayingUI()
    {
        TurnEverythingOff();
        hotbarUI.SetActive(true);
        timerUI.SetActive(true);
        dialogueUI.SetActive(false);
        playUI.SetActive(true);
        SetHearts(3, 3); // change this to access player stats later
        //...
    }

    public void TurnOnMenuUI()
    {
        TurnEverythingOff();
        menuUI.SetActive(true);
    }

    public void TurnOnSettingsUI()
    {
        TurnEverythingOff();
        settingsUI.SetActive(true);
    }

    public void TurnOnJournalUI()
    {
        TurnEverythingOff();
        journalUI.SetActive(true);
        relationshipText.text = "Malice: " + SaveData.saveData.GetMalice() + "\n Honor: " + SaveData.saveData.GetHonor() + "\nKindness: " + SaveData.saveData.GetKindness();
        // Update the bars!!
        maliceSlider.value = SaveData.saveData.GetMalice() / maxRelationshipValue;
        honorSlider.value = SaveData.saveData.GetHonor() / maxRelationshipValue;
        kindnessSlider.value = SaveData.saveData.GetKindness() / maxRelationshipValue;
        UpdateNotesText();
        string notes = "";
        foreach(string s in notesArray)
        {
            notes += s;
            notes += "\n";
        }
        notesText.text = notes;
    }

    public void UpdateNotesText()
    {
        for(int i = 0; i < 8; i++)
        {
            int index = 2 + i;
            if(SaveData.saveData.GetHasMet(i) && SaveData.saveData.GetKnowsNPCSecret(i))
            {
                // knows everything
                if(i == 0)
                {
                    // Borro
                    notesArray[index] = "- Sir Borro: Brave knight allergic to meat";
                }
                else if(i == 1)
                {
                    // Gastou
                    notesArray[index] = "- Gastou: runs with wolves in mountains";
                }
                else if(i == 2)
                {
                    // Pohn
                    notesArray[index] = "- Pohn Jork: insane butcher obsessed with meat";
                }
                else if(i == 3)
                {
                    // Wyl
                    notesArray[index] = "- Wyl: crazy old blacksmith";
                }
                else if(i == 4)
                {
                    // Octavia
                    notesArray[index] = "- Octavia V: manipulative shopkeeper";
                }
                else if(i == 5)
                {
                    // Lupal
                    notesArray[index] = "- Lupal: herbalist who roams the woods at night";
                }
                else if(i == 6)
                {
                    // Thadworn
                    notesArray[index] = "- Thadworn: poor mayor";
                }
                else if (i == 7)
                {
                    // Harry
                    notesArray[index] = "- Harry the barber: ashamed of his baldness";
                }
            }
            else if(SaveData.saveData.GetHasMet(i))
            {
                // knows name
                if(i == 0)
                {
                    // Borro
                    notesArray[index] = "- Sir Borro: ???";
                }
                else if(i == 1)
                {
                    // Gastou
                    notesArray[index] = "- Gastou: ???";
                }
                else if(i == 2)
                {
                    // Pohn
                    notesArray[index] = "- Pohn Jork: ???";
                }
                else if(i == 3)
                {
                    // Wyl
                    notesArray[index] = "- Wyl: ???";
                }
                else if(i == 4)
                {
                    // Octavia
                    notesArray[index] = "- Octavia V: ???";
                }
                else if(i == 5)
                {
                    // Lupal
                    notesArray[index] = "- Lupal: ???";
                }
                else if(i == 6)
                {
                    // Thadworn
                    notesArray[index] = "- Thadworn: ???";
                }
                else if (i == 7)
                {
                    // Harry
                    notesArray[index] = "- Harry the barber: ???";
                }
            }
            else
            {
                notesArray[index] = "- ???: ???";
            }


        }
    }

    public void UpdateDialogue(Sprite sprite, string name, string dialogue, int numOptions, bool isPlayerDialogue)
    {
        TurnOnDialogue();
        dialogueImage.sprite = sprite;
        TypeOutDialogue(name, dialogue);
        if(isPlayerDialogue)
        {
            nextButton.SetActive(false);
            int soFar = 0;
            foreach(GameObject b in dialogueButtons)
            {
                soFar += 1;
                if(soFar <= numOptions)
                {
                    b.SetActive(true);
                }
                else
                {
                    b.SetActive(false);
                }
            }
        }
        else
        {
            // can turn this back on if want the next button, but honestly it's kinda useless I think
            //nextButton.SetActive(true);
            foreach(GameObject b in dialogueButtons)
            {
                b.SetActive(false);
            }
            dialogueButtons[0].SetActive(true);
        }
    }

    public void Testing()
    {
        TurnOnDialogue();
        TypeOutDialogue("Testing 1", "Testing 2 Testing 2 Testing 2 Testing 2 Testing 2 Testing 2 Testing 2 Testing 2 ");
        Debug.Log("Testing 3");
    }

    public void TypeOutDialogue(string header, string bodyText)
    {
        // TO DO: IMPLEMENT THIS FUNCTION TO TYPE OUT THE LETTERS ONE BY ONE SO IT LOOKS COOL :)
        // Google a tutorial on it probably easiest, smth to do with making an array of all the characters I think idk
        // use dialogueName.text to edit the header text and dialogueText.text to edit the body text
        dialogueName.text = header;
        dialogueText.text = "";
        StartCoroutine(TypeLine(bodyText));
    }

    public void TurnOnDialogue()
    {
        hotbarUI.SetActive(false);
        dialogueUI.SetActive(true);
    }
    
    public void TurnOffDialogue()
    {
        hotbarUI.SetActive(true);
        dialogueUI.SetActive(false);
    }

    private IEnumerator TypeLine(string message)
    {
        foreach(char c in message.ToCharArray())
        {
            dialogueText.text += c;
            //Update this to adjust speed (right now the text simply gets faster AS IT PRINTS)
            yield return new WaitForSeconds(textSpeed/dialogueText.text.Length);
        }
    }



// Button Functions
    // PAUSE BUTTONS:

    // Unpause Game
    public void SettingsButton()
    {
        Debug.Log("Set up Settings at some point...");
    }

    public void SaveButton()
    {
        Debug.Log("Set up Save at some point...");
    }

    public void MenuButton()
    {
        Debug.Log("Set up Menu at some point...");
    }

    // OTHER BUTTONS...

// Settings

}