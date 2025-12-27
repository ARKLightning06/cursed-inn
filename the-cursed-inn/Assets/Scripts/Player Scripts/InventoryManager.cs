using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Types for InventoryItems
public enum Category {Weapon, Consumable, Clothing, Accessory, Miscellaneous}




/// <summary>
/// Manages Inventory functionality. Includes opening and closing inventory, and using UIManager to pause and unpause the game at the same time. 
/// Also keeps track of all the different items and various attributes for each one.
/// Also works with the Journal, which keeps track of achievements and clues etc.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    private InputSystem_Actions controls;
    private bool inventoryOpened = false;
    public UIManager uiManager;
    public List<InventoryItem> InventoryGrid = new List<InventoryItem>();

    // The below list is a list of game objects to be transformed into inventory items at start, just a way to populate the inventory with example items for now
    public List<GameObject> starterItems = new List<GameObject>();

    // Temporary measure for inventory slots (will generate them later, maybe)
    public List<Image> slots = new List<Image>();

// Set up controls
    private void Awake()
    {
        // Initialize the InputSystem_Actions() instance
        controls = new InputSystem_Actions();
        
        // Subscribe to the Inventory action
        controls.UI.Inventory.performed += ctx => Inventory();

        // Initialize starterItems into InventoryItems list
        for(int i = 0; i < starterItems.Count; i++)
        {
            InventoryGrid.Add(objectToItem(starterItems[i]));
        }

        // Toggle Slots off to start
        toggleSlots(false);
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

// Open and Close inventory
    private void Inventory()
    {
        if(!inventoryOpened)
        {
            uiManager.PauseGame();
            uiManager.setInventoryOpened(true);
            OpenInventory();
        }
        else
        {
            uiManager.setInventoryOpened(false);
            uiManager.UnpauseGame();
            CloseInventory();
        }
    }
    private void OpenInventory()
    {
        inventoryOpened = true;
        toggleSlots(true);
    }
    
    private void CloseInventory()
    {
        inventoryOpened = false;
        toggleSlots(false);
    }

    private void toggleSlots(bool isOn)
    {
        for(int i = 0; i < slots.Count; i++)
        {
            //TO DO: sort the InventoryGrid based on some criteria before setting visualizations
            //TO DO: figure out how to get the slot thingy as the background, overrideSprite isn't right but good enough for now lol
            Image slot = slots[i];
            slot.enabled = isOn;
            if (i < InventoryGrid.Count)
            {
                slot.overrideSprite = InventoryGrid[i].visualization;
            }
        }
    }

//Manage Inventory
    public InventoryItem objectToItem(GameObject gameObject)
    {
        var stats = gameObject.GetComponent<ItemStats>();
        InventoryItem newItem = new InventoryItem(gameObject, stats.image, stats.description, stats.cost, stats.quantity, stats.itemCat);
        return newItem;
    }
    

// InventoryItem class to contain items in the inventory
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject item;
        public Sprite visualization;
        public string description;
        public int cost;
        public int quantity;
        public Category itemCat;

        public InventoryItem(GameObject theItem, Sprite theVisualization, string theDescription, int theCost, int theQuantity, Category cat)
        {
            item = theItem;
            visualization = theVisualization;
            description = theDescription;
            cost = theCost;
            quantity = theQuantity;
            itemCat = cat;
        }
        
    }

}