using System.Xml.Serialization;
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

    // NPC base stats
    [Header("Base stats")]
    [SerializeField] float movementSpeed;
    [SerializeField] float runMultiplier;
    float appliedSpeed;

    // idle variables
    [Header("Idle stats")]
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    [SerializeField] float leaveTime;

    // patrol variables
    [Header("Patrol stats")]
    [SerializeField] Transform[] waypoints;

    // chase variables
    [Header("Chase stats")]
    [SerializeField] Transform[] returnSpots;

    // sense variables
    Eyes eyes;
    Ears ears;
    bool canSeePlayer;
    bool canHearPlayer;

    // hash variables
    int isPatrolingHash;
    int isChasingHash;

    // getter and setter
    public Animator Animator { get { return animator; } }
    public int IsPatrolingHash { get { return isPatrolingHash; } }
    public int IsChasingHash { get { return isChasingHash; } }
    public bool CanSeePlayer { get { return canSeePlayer; } }
    public bool CanHearPlayer { get { return canHearPlayer; } }
    public float LeaveTIme { get { return leaveTime; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float AppliedSpeed { set { appliedSpeed = value; } }

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
        // set speed to not 0 or else NPC won't move
        movementSpeed = (movementSpeed == 0) ? 1 : movementSpeed;
        runMultiplier = (runMultiplier == 0) ? 2 : runMultiplier;
    }
    void Update()
    {
        currentState.UpdateStates();
    }
}