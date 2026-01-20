using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public GameObject playerVis;
    public UIManager uiManager;
    public InventoryManager inventoryManager;
    private InputSystem_Actions controls;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public int weaponSwingTime;
    public bool isAttacking;
    public List<GameObject> accessibleInventory = new List<GameObject>();


    //public WeaponAnimationController _weaponAnimationController;
    private Animator weaponAnimator;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;


    private void Awake()
    {
        // Initialize the InputSystem_Actions() instance
        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx => moveInput =
          ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput =
          Vector2.zero;
        //controls.Player.Move.performed += ctx => Move();

        controls.Player.Attack.performed += ctx => ClickPressed();
        controls.Player.One.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(1);
        controls.Player.Two.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(2);
        controls.Player.Three.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(3);
        controls.Player.Four.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(4);
        controls.Player.Five.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(5);
        controls.Player.Six.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(6);
        controls.Player.Seven.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(7);
        controls.Player.Eight.performed += ctx => inventoryManager.SetEquippedItemFromHotbar(8);


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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        if (!isAttacking)
        {
            ChangeDirection(rb.linearVelocityX, rb.linearVelocityY);
        }
    }

    private void Move()
    {
        // Apply horizontal movement based on input
        // (Original code): rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    public void ClickPressed()
    {
        if (uiManager.currentState == GameState.Playing)
        {
            Attack();
        }
        else if (uiManager.currentState == GameState.Inventory)
        {
            //Player Script
        }
    }

    public void Attack()
    {
        Debug.Log("Hi");
        Debug.Log(inventoryManager.equippedItemStats.itemCat);
        if (inventoryManager.equippedItem == null)
        {
            Debug.Log("Nothin Equipped");
            //fill in later
        }
        else if (inventoryManager.equippedItemStats.itemCat == Category.Weapon)
        {
            Debug.Log("Hello!");
            isAttacking = true;
            DoAnimation();
        }
    }

    public async Task DoAnimation()
    {
        //inventoryManager.equippedItem.SetActive(true);

        weaponAnimator = inventoryManager.equippedItem.GetComponent<Animator>();
        weaponAnimator.SetBool("Swing", true);
        await Task.Delay(weaponSwingTime); //waits for 0.2 seconds
        isAttacking = false;
        weaponAnimator.SetBool("Swing", false);
        //inventoryManager.equippedItem.SetActive(false);
    }

    public void ChangeDirection(float x, float y)
    {
        if (x > 0)
        {
            if (y > 0)
            {
                //up and to right
                //add animator
                //add weapon direction
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, 45f);

            }
            else if (y == 0)
            {
                //right(default)
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                //down and to the right
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, -45f);
            }
        }
        else if (x == 0)
        {
            if (y > 0)
            {
                //up
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }
            else if (y == 0)
            {
                //Nothing
            }
            else
            {
                //down
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, -90f);
            }
        }
        else
        {
            if (y > 0)
            {
                //up and to left
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, 135f);
            }
            else if (y == 0)
            {
                //left
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            }
            else
            {
                //down and to the left
                playerVis.transform.eulerAngles = new Vector3(0f, 0f, 225f);
            }
        }
    }

    public void UpdateAccessibleInventory(GameObject equipped)
    {
        foreach(GameObject x in accessibleInventory)
        {
            x.SetActive(false);
        }
        if (accessibleInventory.Contains(equipped) && equipped != inventoryManager.equippedItem)
        {
            Debug.Log("is in it");
            equipped.SetActive(true);
        }
        else
        {
            Debug.Log("Not in it");
        }
    }

}

