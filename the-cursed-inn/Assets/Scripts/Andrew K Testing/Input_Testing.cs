using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    //private bool isJumping = false;
    private bool isPaused = false;

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

    private void Pause()
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

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Debug.Log("Paused");
    }
    
    private void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Debug.Log("Unpaused");
    }
}