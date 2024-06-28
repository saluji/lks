using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // state variables
    PlayerBaseState currentState;
    PlayerStateFactory states;

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

    // hash variables
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isFallingHash;

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
    bool requireNewJumpPress = false;

    // getter and setter
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public CharacterController CharacterController { get { return characterController; } }
    public Animator Animator { get { return animator; } }
    public Vector2 CurrentMovementInput { get { return currentMovementInput; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsRunningHash { get { return isRunningHash; } }
    public int IsJumpingHash { get { return isJumpingHash; } }
    public int IsFallingHash { get { return isFallingHash; } }
    public bool IsJumping { set { isJumping = value; } }
    public bool IsJumpPressed { get { return isJumpPressed; } }
    public bool IsFalling { get { return isFalling; } set { isFalling = value; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool RequireNewJumpPress { get { return requireNewJumpPress; } set { requireNewJumpPress = value; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
    public float AppliedMovementY { get { return appliedMovement.y; } set { appliedMovement.y = value; } }
    public float AppliedMovementX { get { return appliedMovement.x; } set { appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return appliedMovement.z; } set { appliedMovement.z = value; } }
    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    public float GroundedGravity { get { return groundedGravity; } }
    public float Gravity { get { return gravity; } }
    public float FallMultiplier { get { return fallMultiplier; } }
    public float TerminalVelocity { get { return terminalVelocity; } }

    void Awake()
    {
        // set initial reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // setup state
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        // set hash reference
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHash = Animator.StringToHash("isFalling");

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

    // callback handler function to set the player input values
    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // callback handler function for run button
    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    // callback handler function for jump button
    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        requireNewJumpPress = false;
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
        currentState.UpdateStates();
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
