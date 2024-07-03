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
    int isWalkingHash;
    int isFleeingHash;
    int isDyingHash;

    // getter and setter
    public NPCBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    
    void Awake()
    {
        states = new NPCStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();
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
}
