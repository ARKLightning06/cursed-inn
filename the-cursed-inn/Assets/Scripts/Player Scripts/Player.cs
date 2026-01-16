using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public UIManager uiManager;
    public InventoryManager inventoryManager;
    private InputSystem_Actions controls;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public WeaponAnimationController _weaponAnimationController;
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
            Debug.Log("playerScript");

        }
    }

    public void Attack()
    {

        if (inventoryManager.equippedItem == null)
        {
            Debug.Log("Nothin Equipped");
            //fill in later
        }
        else if (inventoryManager.equippedItemStats.itemName == "Silver Sword")
        {

            DoAnimation();
            Debug.Log("Silver Sword swung");
        }
        else if (inventoryManager.equippedItemStats.itemName == "Axe")
        {
            DoAnimation();
        }
    }

    public async Task DoAnimation()
    {
        //inventoryManager.equippedItem.SetActive(true);
        weaponAnimator = inventoryManager.equippedItem.GetComponent<Animator>();
        weaponAnimator.SetBool("Swing", true);
        await Task.Delay(200); //waits for 0.2 seconds
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
            }
            else if (y == 0)
            {
                //right

            }
            else
            {
                //down and to the right
            }
        }
        else if (x == 0)
        {
            if (y > 0)
            {
                //up    
            }
            else if (y == 0)
            {
                //Nothing
            }
            else
            {
                //down
            }
        }
        else
        {
            if (y > 0)
            {
                //up and to left   
            }
            else if (y == 0)
            {
                //left
            }
            else
            {
                //down and to the left
            }
        }
    }

}

