using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : MonoBehaviour
{
    // state variables
    NPCBaseState currentState;
    NPCStateFactory states;

    // reference variables
    CharacterController characterController;
    NavMeshAgent agent;
    Animator animator;
    Eyes eyes;
    Ears ears;

    // NPC stats
    [Header("NPC values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float runMultiplier;
    Vector3 appliedSpeed;

    // idle variables
    [Header("Idle values")]
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    float leaveTime;

    // patrol variables
    [Header("Patrol values")]
    [SerializeField] Transform[] waypoints;
    Vector3 targetPosition;
    int currentWaypointIndex;

    // chase variables
    [Header("Chase values")]
    [SerializeField] Transform[] returnSpots;

    // hash variables
    int isPatrolingHash;
    int isChasingHash;

    // getter and setter
    public CharacterController CharacterController { get { return characterController; } set { characterController = value; } }
    public NavMeshAgent Agent { get { return agent; } }
    public Animator Animator { get { return animator; } }
    public Eyes Eyes { get { return eyes; } }
    public Ears Ears { get { return ears; } }
    public Vector3 TargetPosition { get { return targetPosition; } set { targetPosition = value; } }
    public Transform[] Waypoints { get { return waypoints; } }
    public int CurrentWaypointIndex { get { return currentWaypointIndex; } set { currentWaypointIndex = value; } }
    public int IsPatrolingHash { get { return isPatrolingHash; } }
    public int IsChasingHash { get { return isChasingHash; } }
    public float LeaveTIme { get { return leaveTime; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float MinWaitTime { get { return minWaitTime; } }
    public float MaxWaitTime { get { return maxWaitTime; } }
    public float LeaveTime { get { return leaveTime; } set { leaveTime = value; } }
    public float AppliedSpeedX { get { return appliedSpeed.x; } set { appliedSpeed.x = value; } }
    public float AppliedSpeedZ { get { return appliedSpeed.z; } set { appliedSpeed.z = value; } }

    void Awake()
    {
        // set initial reference variable
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        eyes = GetComponentInChildren<Eyes>();
        ears = GetComponentInChildren<Ears>();

        // setup state
        states = new NPCStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();

        // set has reference
        isPatrolingHash = Animator.StringToHash("isPatroling");
        isChasingHash = Animator.StringToHash("isChasing");
    }

    void Start()
    {
        // set speed to not 0 otherwise NPC won't move
        movementSpeed = (movementSpeed == 0) ? 1 : movementSpeed;
        runMultiplier = (runMultiplier == 0) ? 2 : runMultiplier;
    }
    void Update()
    {
        Movement();
        currentState.UpdateStates();
    }
    void Movement()
    {
        characterController.Move(appliedSpeed * Time.deltaTime);
    }
}