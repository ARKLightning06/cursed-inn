using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private InputSystem_Actions controls;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    //private bool isJumping = false;

    public WeaponAnimationController _weaponAnimationController;
    private Animator weaponAnimator;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;

    private void Awake()
    {
        // Initialize the InputSystem_Actions() instance
        controls = new InputSystem_Actions();

        // Subscribe to the movement and jump actions
        controls.Player.Move.performed += ctx => moveInput =
          ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput =
          Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Attack.performed += ctx => Attack();

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

    private void Jump()
    {
        //ORIGINAL CODE:
        //(Useless because we don't jump in topdown, just included for testing purposes for now, should be cleaned up later)
        // Check if the player is grounded before jumping
        // if (IsGrounded())
        //{
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //}
        Debug.Log("Space Pressed!");
    }

    private bool IsGrounded()
    {
        // Use a simple ground check to see if the player is touching 
        // the ground
        RaycastHit2D hit = Physics2D.Raycast
          (transform.position, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    public void Attack()
    {
        if (inventoryManager.equippedItemStats.itemName == "Sword")
        {
            //_weaponAnimationController.SwingSword();
            weaponAnimator = inventoryManager.equippedItem.GetComponent<Animator>();
            DoAnimation();
        }
        else if (inventoryManager.equippedItemStats.itemName == "Axe")
        {
            weaponAnimator = inventoryManager.equippedItem.GetComponent<Animator>();
            DoAnimation();
        }
    }

    public async Task DoAnimation()
    {
        weaponAnimator.SetBool("Swing", true);
        await Task.Delay(200); //waits for 0.2 seconds
        weaponAnimator.SetBool("Swing", false);
    }

}

