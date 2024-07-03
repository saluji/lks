using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    #region Variables
    // state variables
    PlayerBaseState currentState;
    PlayerStateFactory states;

    // reference variables
    PlayerInput playerInput;
    CharacterController characterController;
    WifeyStateMachine wifey;
    KnightStateMachine knight;
    NPCStateMachine npc;
    Animator animator;
    GameManager gameManager;
    AudioManager audioManager;
    UIManager uIManager;
    Transform mouth;
    [SerializeField] GameObject fireball;

    // store input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    Vector3 cameraRelativeMovement;
    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed = false;
    bool isJumpable = true;
    bool requireNewJumpPress = false;
    bool isFalling;
    bool isSnatchPressed;
    bool isConsumePressed;
    bool isAttackPressed;
    bool isStompPressed;
    bool isActionable;

    // player stats
    [Header("Player values")]
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float runMultiplier = 1f;
    [SerializeField] float turnSpeed = 1.0f;
    [SerializeField] int maxHP = 100;
    [SerializeField] int healAmount = 10;

    // gravity stats
    [Header("Gravity values")]
    [SerializeField] float fallMultiplier = 2.0f;
    [SerializeField] float terminalVelocity = -20.0f;
    float gravity = -1f;

    // jump stats
    [Header("Jump values")]
    [SerializeField] float maxJumpHeight = 1.0f;
    [SerializeField] float maxAirTime = 0.5f;
    float initialJumpVelocity;
    float timeToApex;

    // hash variables
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isDyingHash;
    int isSnatchingHash;
    int isConsumingHash;
    int isAttackingHash;
    int isStompingHash;

    // NPC consumption counter
    int consumeCounter = 0;
    int maxNPC = 100;

    // animation length
    float animationLength = 0;

    #endregion

    #region Getter and Setter
    public GameManager GameManager { get { return gameManager; } }
    public GameObject Fireball { get { return fireball; } }
    public AudioManager AudioManager { get { return audioManager; } }
    public UIManager UIManager { get { return uIManager; } }
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public CharacterController CharacterController { get { return characterController; } }
    public Animator Animator { get { return animator; } }
    public Transform Mouth { get { return mouth; } }
    public Vector2 CurrentMovementInput { get { return currentMovementInput; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsRunningHash { get { return isRunningHash; } }
    public int IsJumpingHash { get { return isJumpingHash; } }
    public int IsSnatchingHash { get { return isSnatchingHash; } }
    public int IsConsumingHash { get { return isConsumingHash; } }
    public int IsDyingHash { get { return isDyingHash; } }
    public int IsAttackingHash { get { return isAttackingHash; } }
    public int IsStompingHash { get { return isStompingHash; } }
    public int ConsumeCounter { get { return consumeCounter; } set { consumeCounter = value; } }
    public int MaxNPC { get { return maxNPC; } }
    public int HealAmount { get { return healAmount; } }
    public bool IsJumpable { get { return isJumpable; } set { isJumpable = value; } }
    public bool IsJumpPressed { get { return isJumpPressed; } }
    public bool IsFalling { get { return isFalling; } set { isFalling = value; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool IsSnatchPressed { get { return isSnatchPressed; } }
    public bool IsConsumePressed { get { return isConsumePressed; } }
    public bool IsAttackPressed { get { return isAttackPressed; } }
    public bool RequireNewJumpPress { get { return requireNewJumpPress; } set { requireNewJumpPress = value; } }
    public bool IsActionable { get { return isActionable; } set { isActionable = value; } }
    public bool IsStompPressed { get { return isStompPressed; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float CurrentMovementY { get { return currentMovement.y; } set { currentMovement.y = value; } }
    public float AppliedMovementY { get { return appliedMovement.y; } set { appliedMovement.y = value; } }
    public float AppliedMovementX { get { return appliedMovement.x; } set { appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return appliedMovement.z; } set { appliedMovement.z = value; } }
    public float TurnSpeed { set { turnSpeed = value; } }
    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    public float Gravity { get { return gravity; } }
    public float FallMultiplier { get { return fallMultiplier; } }
    public float TerminalVelocity { get { return terminalVelocity; } }
    public float AnimationLength { get { return animationLength; } set { animationLength = value; } }
    #endregion


    void Awake()
    {
        // set initial reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        wifey = GameObject.Find("Wifey").GetComponent<WifeyStateMachine>();
        knight = GameObject.Find("Knight").GetComponent<KnightStateMachine>();
        npc = GameObject.Find("NPC").GetComponent<NPCStateMachine>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        mouth = GameObject.Find("Mouth").transform;

        // set max HP value
        // uIManager.PlayerHP.maxValue = maxHP;
        uIManager.PlayerHP.maxValue = uIManager.PlayerHP.value = maxHP;

        // setup state
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        // set hash reference
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isDyingHash = Animator.StringToHash("isDying");
        isSnatchingHash = Animator.StringToHash("isSnatching");
        isConsumingHash = Animator.StringToHash("isConsuming");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isStompingHash = Animator.StringToHash("isStomping");

        // set player input callbacks
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.performed += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;
        playerInput.CharacterControls.Snatch.started += OnSnatch;
        playerInput.CharacterControls.Snatch.canceled += OnSnatch;
        playerInput.CharacterControls.Consume.started += OnConsume;
        playerInput.CharacterControls.Consume.canceled += OnConsume;
        playerInput.CharacterControls.Attack.started += OnAttack;
        playerInput.CharacterControls.Attack.canceled += OnAttack;
        playerInput.CharacterControls.Stomp.started += OnStomp;
        playerInput.CharacterControls.Stomp.canceled += OnStomp;

        SetupJumpVariables();
    }

    #region Callback handler function
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
        requireNewJumpPress = false;
    }

    void OnSnatch(InputAction.CallbackContext context)
    {
        isSnatchPressed = context.ReadValueAsButton();
    }
    void OnConsume(InputAction.CallbackContext context)
    {
        isConsumePressed = context.ReadValueAsButton();
    }
    void OnAttack(InputAction.CallbackContext context)
    {
        isAttackPressed = context.ReadValueAsButton();
    }
    void OnStomp(InputAction.CallbackContext context)
    {
        isStompPressed = context.ReadValueAsButton();
    }
    #endregion

    void SetupJumpVariables()
    {
        // set initial jump variables with gravitational fall equation
        maxAirTime = (maxAirTime <= 0) ? 1 : maxAirTime;
        timeToApex = maxAirTime / 2;
        gravity = -2 * maxJumpHeight / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = 2 * maxJumpHeight / timeToApex;
    }

    void Start()
    {
        characterController.Move(appliedMovement * Time.deltaTime);
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        currentState.UpdateStates();
    }

    void HandleMovement()
    {
        // move player relative to camera position
        cameraRelativeMovement = ConvertToCameraSpace(appliedMovement);

        // move player and calculate speed if moving and / or running
        characterController.Move(cameraRelativeMovement * Time.deltaTime);
    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        // store the Y value of the original vector to rotate
        float currentYValue = vectorToRotate.y;

        // get forward and right directional vectors of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // remove the Y values to ignore upward / downward camera angles
        cameraForward.y = 0;
        cameraRight.y = 0;

        // re-normalize both vectors so they each have a magnitude of 1
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // rotate the X and Z VectorToRotate values to camera space
        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        // the sum of both products is the Vector3 in camera space
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = cameraRelativeMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = cameraRelativeMovement.z;

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

    void OnTriggerStay(Collider collider)
    {
        // currentState.OnTriggerEnter(collider);
        // if (collider.gameObject.CompareTag("Wifey"))
        // {
        //     wifey.IncreaseHP(healAmount);
        // }
        // if (collider.gameObject.CompareTag("NPC") && isSnatchPressed)
        // {
        //     Destroy(collider.gameObject);
        // }
    }
    public void IncreaseHP(int amount)
    {
        uIManager.PlayerHP.value += amount;
    }
    public void DecreaseHP(int amount)
    {
        uIManager.PlayerHP.value -= amount;
    }
}