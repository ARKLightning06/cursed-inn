using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        TurnOnPlayingUI();
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
        inventoryUI.SetActive(true);
        hotbarUI.SetActive(true);
        inventoryManager.ToggleSlots(true);
    }

    public void TurnOnPauseMenuUI()
    {
        TurnEverythingOff();
        pauseUI.SetActive(true);
        //...
    }

    public void TurnOnPlayingUI()
    {
        TurnEverythingOff();
        hotbarUI.SetActive(true);
        playUI.SetActive(true);
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