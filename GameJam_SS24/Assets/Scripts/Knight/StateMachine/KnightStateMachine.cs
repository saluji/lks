using UnityEngine;
using UnityEngine.AI;

public class KnightStateMachine : MonoBehaviour
{
    // state variables
    KnightBaseState currentState;
    KnightStateFactory states;

    // reference variables
    NavMeshAgent agent;
    Animator animator;
    Eyes eyes;
    Sense sense;

    // knight stats
    [Header("NPC values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float runMultiplier;

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

    // hash variables
    int isPatrolingHash;
    int isChasingHash;
    int isAttackingHash;
    int isDyingHash;

    float animationLength;

    // getter and setter
    public KnightBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public NavMeshAgent Agent { get { return agent; } }
    public Animator Animator { get { return animator; } }
    public Sense Sense { get { return sense; } }
    public Eyes Eyes { get { return eyes; } }
    public Vector3 TargetPosition { get { return targetPosition; } set { targetPosition = value; } }
    public Transform[] Waypoints { get { return waypoints; } }
    public int CurrentWaypointIndex { get { return currentWaypointIndex; } set { currentWaypointIndex = value; } }
    public int IsPatrolingHash { get { return isPatrolingHash; } }
    public int IsChasingHash { get { return isChasingHash; } }
    public int IsAttackingHash { get { return isAttackingHash; } }
    public int IsDyingHash { get { return isDyingHash; } }
    public float LeaveTIme { get { return leaveTime; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float MinWaitTime { get { return minWaitTime; } }
    public float MaxWaitTime { get { return maxWaitTime; } }
    public float LeaveTime { get { return leaveTime; } set { leaveTime = value; } }
    public float AnimationLength { get { return animationLength; } set { animationLength = value; } }

    void Awake()
    {
        // set initial reference variable
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sense = GetComponentInChildren<Sense>();
        eyes = GetComponentInChildren<Eyes>();

        // setup state
        states = new KnightStateFactory(this);
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

    // set agent velocity
    public void SetAgentSpeed(float patrolSpeed, float chaseSpeed)
    {
        agent.speed = patrolSpeed * chaseSpeed;
    }

    // set agent destination
    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(collider);
    }
    void OnTriggerExit(Collider collider)
    {
        currentState.OnTriggerExit(collider);
    }
}