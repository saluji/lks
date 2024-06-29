using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : MonoBehaviour
{
    // state variables
    NPCBaseState currentState;
    NPCStateFactory states;

    // reference variables
    NavMeshAgent agent;
    Animator animator;

    // NPC stats
    [Header("NPC values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float runMultiplier;
    float appliedSpeed;

    // idle variables
    [Header("Idle values")]
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;

    // patrol variables
    [Header("Patrol values")]
    [SerializeField] Transform[] waypoints;
    float leaveTime;

    // chase variables
    [Header("Chase values")]
    [SerializeField] Transform[] returnSpots;

    // sense variables
    Eyes eyes;
    Ears ears;

    // hash variables
    int isPatrolingHash;
    int isChasingHash;

    // getter and setter
    public Animator Animator { get { return animator; } }
    public Eyes Eyes { get { return eyes; } }
    public Ears Ears { get { return ears; } }
    public int IsPatrolingHash { get { return isPatrolingHash; } }
    public int IsChasingHash { get { return isChasingHash; } }
    public float LeaveTIme { get { return leaveTime; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float AppliedSpeed { get { return appliedSpeed; } set { appliedSpeed = value; } }
    public float MinWaitTime { get { return minWaitTime; } }
    public float MaxWaitTime { get { return maxWaitTime; } }
    public float LeaveTime { get { return leaveTime; } set { leaveTime = value; } }

    void Awake()
    {
        // set initial reference variable
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        eyes = GetComponentInChildren<Eyes>();
        ears = GetComponentInChildren<Ears>();
        appliedSpeed = agent.speed;

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
        currentState.UpdateStates();
    }
}