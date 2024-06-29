using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : MonoBehaviour
{
    // state variables
    // NPCBaseState currentState;
    // NPCStateFactory states;

    // reference variables
    Transform player;
    Vector3 playerPosition;
    NavMeshAgent agent;
    Animator animator;
    float initialAgentSpeed;

    // senses variables
    Eyes eyes;
    Ears ears;
    bool canSeePlayer;
    bool canHearPlayer;

    // idle variables
    [Header("Idle stats")]
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    [SerializeField] float leaveTime;

    // patrol variables
    [Header("Patrol stats")]
    [SerializeField] Transform waypoints;
    int currentWaypointIndex;
    Vector3 targetPosition;

    // chase variables
    [Header("Chase stats")]
    [SerializeField] Transform[] hidingSpots;


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

    void Awake()
    {
        // set initial reference variable
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        initialAgentSpeed = agent.speed;
        animator = GetComponent<Animator>();

        eyes = GetComponentInChildren<Eyes>();
        ears = GetComponentInChildren<Ears>();

        // set has reference
        isPatrolingHash = Animator.StringToHash("isPatroling");
        isChasingHash = Animator.StringToHash("isChasing");
    }

}