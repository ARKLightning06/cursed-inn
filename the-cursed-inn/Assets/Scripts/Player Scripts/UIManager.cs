using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Class to manager UI functionality
// This includes pausing and unpausing the game, keeping track of buttons and other elements of UI, and adjusting setttings.
public class UIManager : MonoBehaviour
{
    private InputSystem_Actions controls;
    private bool isPaused = false;
    private bool inventoryIsOpened = false;

// Setting up Input
    private void Awake()
    {
        // Initialize the InputSystem_Actions() instance
        controls = new InputSystem_Actions();
        
        // Subscribe to the Inventory action
        controls.UI.Pause.performed += ctx => Pause();
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
        if(!isPaused)
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
        if (inventoryIsOpened)
        {
            return;
        }
        isPaused = true;
        Time.timeScale = 0f;
        Debug.Log("Paused");
    }
    
    public void UnpauseGame()
    {
        if (inventoryIsOpened)
        {
            return;
        }
        isPaused = false;
        Time.timeScale = 1f;
        Debug.Log("Unpaused");
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void setInventoryOpened(bool isOpened)
    {
        inventoryIsOpened = isOpened;
    }

// Buttons and other UI


// Settings

}