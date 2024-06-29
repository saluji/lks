using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : BaseStateMachine
{
    public Vector3 PlayerPosition { get => _player.position; }
    public bool CanSeePlayer { get => _eyes.IsDetecting; }
    public bool CanHearPlayer { get => _ears.IsDetecting;  }

    public NPCIdleState IdleState;
    public NPCPatrolState PatrolState;
    public NPCFleeState FleeState;
    public NPCHideState HideState;

    private Eyes _eyes;
    private Ears _ears;

    private Transform _player;
    private NavMeshAgent _agent;
    private Animator _animator;

    private float _initialAgentSpeed;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        WaypointGizmos.DrawWayPoints(PatrolState.Waypoints);
    }
#endif
    public override void Initialize()
    {
        _eyes = GetComponentInChildren<Eyes>();
        _ears = GetComponentInChildren<Ears>();

        _player = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _initialAgentSpeed = _agent.speed;
        _animator = GetComponent<Animator>();

        CurrentState = IdleState;
        CurrentState.OnEnterState(this);
    }

    public override void Tick()
    {
        _animator.SetFloat("speed", _agent.velocity.magnitude);
    }

    public void SetDestination(Vector3 destination) 
    {
        _agent.SetDestination(destination);
    }

    public void SetAgentSpeedMultiplier(float multiplier) 
    {
        _agent.speed = _initialAgentSpeed * multiplier;
    }

}
