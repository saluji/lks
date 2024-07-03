using UnityEngine;
using UnityEngine.AI;

public class KnightStateMachine : MonoBehaviour
{
    // state variables
    KnightBaseState currentState;
    KnightStateFactory states;

    // reference variables
    PlayerStateMachine player;
    NavMeshAgent agent;
    Animator animator;
    Eyes eyes;
    Sense sense;
    WifeyStateMachine wifey;

    // knight stats
    [Header("NPC values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float runMultiplier;
    [SerializeField] int damage;
    [SerializeField] int increaseHP;

    // hash variables
    int isPatrolingHash;
    int isChasingHash;
    int isAttackingHash;
    int isDyingHash;
    int isEatenHash;

    float animationLength;

    // getter and setter
    public KnightBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public PlayerStateMachine PlayerStateMachine { get { return player; } }
    public WifeyStateMachine WifeyStateMachine { get { return wifey; } }
    public NavMeshAgent Agent { get { return agent; } }
    public Animator Animator { get { return animator; } }
    public Sense Sense { get { return sense; } }
    public Eyes Eyes { get { return eyes; } }
    public int IsPatrolingHash { get { return isPatrolingHash; } }
    public int IsChasingHash { get { return isChasingHash; } }
    public int IsAttackingHash { get { return isAttackingHash; } }
    public int IsDyingHash { get { return isDyingHash; } }
    public int IsEatenHash { get { return isEatenHash; } }
    public int Damage { get { return damage; } }
    public int IncreaseHP { get { return increaseHP; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float AnimationLength { get { return animationLength; } set { animationLength = value; } }

    void Awake()
    {
        // set initial reference variable
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sense = GetComponentInChildren<Sense>();
        eyes = GetComponentInChildren<Eyes>();
        player = GameObject.Find("Player").GetComponent<PlayerStateMachine>();
        wifey = GameObject.Find("Wifey").GetComponent<WifeyStateMachine>();

        // setup state
        states = new KnightStateFactory(this);
        currentState = states.Patrol();
        currentState.EnterState();

        // set has reference
        isPatrolingHash = Animator.StringToHash("isPatroling");
        isChasingHash = Animator.StringToHash("isChasing");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isDyingHash = Animator.StringToHash("isDying");
        isEatenHash = Animator.StringToHash("isEaten");
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

    void OnTriggerStay(Collider collider)
    {
        currentState.OnTriggerStay(collider);
    }
}