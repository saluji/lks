using System;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // player stats
    [Header("Player behaviour")]
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float runMultiplier = 1f;
    [SerializeField] float turnSpeed = 1.0f;

    // reference variables
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    // store input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;
    int isWalkingHash;
    int isRunningHash;

    // gravity stats
    [Header("Gravity values")]
    [SerializeField] float fallMultiplier = 2.0f;
    [SerializeField] float terminalVelocity = -20.0f;
    float gravity = -1f;
    float groundedGravity = -0.5f;
    bool isFalling;

    // jump stats
    [Header("Jump variables")]
    [SerializeField] float maxJumpHeight = 1.0f;
    [SerializeField] float maxJumpTime = 0.5f;
    float initialJumpVelocity;
    float timeToApex;
    bool isJumpPressed = false;
    bool isJumping = false;
    int isJumpingHash;

    void Awake()
    {
        // set initial reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // set hash reference
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        // set player input callbacks
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;

        SetupJumpVariables();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void SetupJumpVariables()
    {
        // set initial jump variables with gravitational fall equation
        maxJumpTime = (maxJumpTime == 0) ? 1 : maxJumpTime;
        timeToApex = maxJumpTime / 2;
        gravity = -2 * maxJumpHeight / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = 2 * maxJumpHeight / timeToApex;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleAnimation();
        HandleGravity();
        HandleJump();
    }

    void HandleMovement()
    {
        // move player and calculate speed if moving and / or running
        appliedMovement = new Vector3
        (
            (isRunPressed ? currentRunMovement.x : currentMovement.x) * movementSpeed,
            appliedMovement.y,
            (isRunPressed ? currentRunMovement.z : currentMovement.z) * movementSpeed
        );
        characterController.Move(appliedMovement * Time.deltaTime);
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        // turn player depending on inputs
        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        // locomotion animation logic
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (isMovementPressed && isRunPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void HandleGravity()
    {
        // instantly fall if letting go of jump button
        isFalling = currentMovement.y <= 0.0f || !isJumpPressed;

        // calculate gravity with velocity verlet integration
        if (isFalling)
        {
            // additional gravity applied after reaching the apex of jump
            float previousYVelocity = currentMovement.y;
            currentMovement.y += gravity * fallMultiplier * Time.deltaTime;
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * 0.5f, terminalVelocity);
        }
        else
        {
            // applied when character not grounded
            float previousYVelocity = currentMovement.y;
            currentMovement.y += gravity * Time.deltaTime;
            appliedMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
        }
    }

    void HandleJump()
    {
        // jump logic and animation
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            appliedMovement.y = initialJumpVelocity;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void OnEnable()
    {
        // enable the character controls action map
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        // disable the character controls action map
        playerInput.CharacterControls.Disable();
    }
}
