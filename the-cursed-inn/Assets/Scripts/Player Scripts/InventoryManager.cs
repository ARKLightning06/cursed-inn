using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Types for InventoryItems
public enum Category {Accessory, Clothing, Consumable, Miscellaneous, Weapon}




/// <summary>
/// Manages Inventory functionality. Includes opening and closing inventory, and using UIManager to pause and unpause the game at the same time. 
/// Also keeps track of all the different items and various attributes for each one.
/// Also works with the Journal, which keeps track of achievements and clues etc.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    private InputSystem_Actions controls;

    [Header("Setup")]
    public UIManager uiManager;
    // The below list is a list of game objects to be transformed into inventory items at start, just a way to populate the inventory with example items for now
    public List<GameObject> starterItems = new List<GameObject>();
    // Empty Inventory Slots, ensure they are connected to the buttons
    public List<Button> slots = new List<Button>();
    // Panels for Inventory, Descriptor, and Hotbar
    public GameObject inventoryPanel;
    public GameObject descriptorPanel;
    public GameObject hotbarPanel;
    // Text and Image for Descriptor
    public TMP_Text descriptorName;
    public TMP_Text descriptorDescription;
    public Image descriptorImage;
    public Sprite defaultDescriptorImage;
    public string defaultDescriptorHeader;
    public string defaultDescriptorDescription;
    // For setting up the inventory
    public List<InventoryItem> InventoryGrid = new List<InventoryItem>();


    [Header("Info for other Scripts")]

    // Current Item Equipped by the Player
    public GameObject equippedItem;
    public ItemStats equippedItemStats;

    [Header("Info for Inventory Aesthetics")]
    
    public Sprite defaultInventorySprite; // background sprite for an empty slot in the inventory
    public Vector2 defaultWidthHeight; //width height vector for an empty inventory slot
    public Vector2 itemWidthHeight; //width height vector for an item in the inventory when it exists
    public Color emptySlotColor; // Color for empty slots
    public Color fullSlotColor; // Color for full slots (should almost definitely be white)
    public Color panelsColor; // Color for background panels
    private int sortCategory = 0; // Sort category for sorting InventoryItems, where 0 is name, 1 is cost, 2 is quantity, and 3 is category


    private void Awake()
    {
        // Initialize the InputSystem_Actions() instance
        controls = new InputSystem_Actions();
        
        // Subscribe to the Inventory action
        controls.UI.Inventory.performed += ctx => Inventory();

        // Initialize starterItems into InventoryItems list
        for(int i = 0; i < starterItems.Count; i++)
        {
            InventoryGrid.Add(ObjectToItem(starterItems[i]));
            // TO DO: work out quantity so if it already exists it adds to quantity instead of adding new item, same with add item function
        }        

        // Toggle Slots off to start
        ToggleSlots(false);

        // Set the color of the panels to desired color
        inventoryPanel.GetComponent<Image>().color = panelsColor;
        descriptorPanel.GetComponent<Image>().color = panelsColor;
        hotbarPanel.GetComponent<Image>().color = panelsColor;

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
        if(uiManager.currentState != GameState.Inventory)
        {
            uiManager.PauseGame();
            OpenInventory();
        }
        else
        {
            uiManager.UnpauseGame();
            CloseInventory();
        }
    }
    private void OpenInventory()
    {
        uiManager.currentState = GameState.Inventory;
        uiManager.TurnOnInventoryUI();
    }
    
    private void CloseInventory()
    {
        uiManager.currentState = GameState.Playing;
        uiManager.TurnOnPlayingUI();
    }

    public void ToggleSlots(bool isOn)
    {
        if (isOn)
        {
            ArrangeSlots();
        }
        for(int i = 0; i < slots.Count; i++)
        {
            //TO DO: add a function to be associated with items (maybe based on their category? Like weapons and accessories would be equiped in various ways, while consumables are consumed?)
            Button backgroundSlot = slots[i];
            backgroundSlot.gameObject.SetActive(isOn);
            backgroundSlot.onClick.RemoveAllListeners();
            backgroundSlot.GetComponent<Image>().sprite = defaultInventorySprite;
            backgroundSlot.GetComponent<RectTransform>().sizeDelta = defaultWidthHeight;
            ColorBlock cb = backgroundSlot.colors;
            cb.normalColor = emptySlotColor;
            // backgroundSlot.transition = Selectable.Transition.ColorTint;
            if (i < InventoryGrid.Count)
            {
                InventoryItem currentItem = InventoryGrid[i];
                backgroundSlot.GetComponent<Image>().sprite = InventoryGrid[i].visualization; // assigns visualization of associated InventoryItem
                backgroundSlot.onClick.AddListener(() => InventoryFunction(currentItem)); // assigns function onClick... need to map it to associated InventoryItem
                // backgroundSlot.transition = Selectable.Transition.None; // changes transition to none so the sprite coloring isn't messed up
                backgroundSlot.GetComponent<RectTransform>().sizeDelta = itemWidthHeight;
                // TO DO: Make this ^^^ actually work so sprites retain normal coloring when applying visualization, for some reason doesn't work rn...
                cb.normalColor = fullSlotColor;
            }
            backgroundSlot.colors = cb;
        }
    }

    //Arranges background slot buttons and foreground slot icons based on number of items in inventory and slotsPerPage
    public void ArrangeSlots()
    {
        Debug.Log("Arrange Slots...");
        SortItemsBy(sortCategory);
    }

    public void SortItemsBy(int cat)
    {
        sortCategory = cat;
        List<InventoryItem> GridCopy = new List<InventoryItem>();
        List<int> unsorted = new List<int>();
        List<int> sortedIndexes = new List<int>();
        List<string> unsortedStr = new List<string>(); // just for alphabetically organizing
        List<string> sortedStr = new List<string>(); // same
        for (int i = 0; i < InventoryGrid.Count; i++)
        {
            if (cat == 1)
            {
                unsorted.Add(InventoryGrid[i].cost);
                sortedIndexes.Add(InventoryGrid[i].cost);
            }
            else if (cat == 2)
            {
                unsorted.Add(InventoryGrid[i].quantity);
                sortedIndexes.Add(InventoryGrid[i].quantity);
            }
            else if (cat == 3)
            {
                unsorted.Add((int)(InventoryGrid[i].itemCat));
                sortedIndexes.Add((int)(InventoryGrid[i].itemCat));
            }
            else
            {
                unsortedStr.Add(InventoryGrid[i].itemName);
                sortedStr.Add(InventoryGrid[i].itemName);
            }
        }
        sortedIndexes.Sort();
        sortedStr.Sort();
        if (cat != 0)
        {
            for (int i = 0; i < sortedIndexes.Count; i++)
            {
                int index = unsorted.IndexOf(sortedIndexes[i]);
                if (index == -1)
                {
                    Debug.Log("ERROR something messed up in trying to sort items... see InventoryManager script, SortItemsBy function");
                }
                // unsorted.Remove(unsorted[index]); (dumb)
                unsorted[index] = -676767; // probably a better way to do this...?
                GridCopy.Add(InventoryGrid[index]);
                Debug.Log("At index " + index + ", there should be " + InventoryGrid[index].itemName);
            }
        }
        else
        {
            for (int i = 0; i < sortedStr.Count; i++)
            {
                int index = unsortedStr.IndexOf(sortedStr[i]);
                if (index == -1)
                {
                    Debug.Log("ERROR something messed up in trying to sort items... see InventoryManager script, SortItemsBy function");
                }
                // unsortedStr.Remove(unsortedStr[index]); (dumb)
                unsortedStr[index] = "jklfjkladsjfkldjskfjdkjsfkdjskfjsldk"; // again, there's probably a better way to do this... also if there are any items named jklfjkladsjfkldjskfjdkjsfkdjskfjsldk then we're screwed lol
                GridCopy.Add(InventoryGrid[index]);
                Debug.Log("At index " + index + ", there should be " + InventoryGrid[index].itemName);
                Debug.Log("huh?");
            }
        }
        InventoryGrid = GridCopy;

    }

    // use this instead of directly calling ArrangeSlots to avoid infinite recursion loop
    public void SetSortingCategory(int cat)
    {
        sortCategory = cat;
    }

//Manage Inventory
    public InventoryItem ObjectToItem(GameObject gameObject)
    {
        ItemStats stats = gameObject.GetComponent<ItemStats>();
        InventoryItem newItem = new InventoryItem(gameObject, stats.image, stats.itemName, stats.description, stats.cost, stats.quantity, stats.itemCat);
        return newItem;
    }

    public void InventoryFunction(InventoryItem calledItem)
    {
        Debug.Log("A button was clicked! That button was " + calledItem.itemName + ".");
        // need some way to call a function depending on the item... couple ideas, one could make a list of possible item actions (use, draw (weapon), wear (clothes), etc) and have each itemStats script have a string paramter
        // specifying which one to call, two we could make an intermediate step where clicking on the item asks Do you want to use this item? Or something, then clicking the button does one of the list of actions, three somehow have
        // each item have its own function specified by itemStats? not sure how that would work tho
        equippedItem = calledItem.item;
        equippedItemStats = calledItem.item.GetComponent<ItemStats>();
    }

    public void OnHoverEnter(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;
        GameObject hovered = pointer.pointerEnter;
        int index = slots.IndexOf(hovered.GetComponent<Button>());
        if (index >= InventoryGrid.Count)
        {
            SetDescriptorToDefault();
        }
        else
        {
            InventoryItem hoveredItem = InventoryGrid[index];
            SetDescriptorStats(hoveredItem);

        }
    }

    public void SetDescriptorStats(InventoryItem describedItem)
    {
        descriptorName.text = describedItem.itemName;
        descriptorDescription.text = describedItem.description;
        descriptorImage.sprite = describedItem.visualization;
    }

    public void SetDescriptorToDefault()
    {
        descriptorName.text = defaultDescriptorHeader;
        descriptorDescription.text = defaultDescriptorDescription;
        descriptorImage.sprite = defaultDescriptorImage;
    }


    

// InventoryItem class to contain items in the inventory
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject item;
        public Sprite visualization;
        public string itemName;
        public string description;
        public int cost;
        public int quantity;
        public Category itemCat;

        public InventoryItem(GameObject theItem, Sprite theVisualization, string theName, string theDescription, int theCost, int theQuantity, Category cat)
        {
            item = theItem;
            visualization = theVisualization;
            itemName = theName;
            description = theDescription;
            cost = theCost;
            quantity = theQuantity;
            itemCat = cat;
        }
        
    }

}