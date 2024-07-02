using UnityEngine;

public class KnightAttackState : KnightBaseState
{
    public KnightAttackState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Knight Patrol: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, true);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);
        Ctx.TargetPosition = GetNextWaypoint();
        Ctx.SetDestination(Ctx.TargetPosition);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, 1f);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Knight Patrol: Exit");
    }

    public override void CheckSwitchStates()
    {
        // as long as game over is not active
        if (Ctx.Eyes.isDetecting)
        {
            SwitchState(Factory.Chase());
        }

        // switch to idle if reaching waypoint
        float sqrtDistance = (Ctx.transform.position - Ctx.TargetPosition).sqrMagnitude;
        if (sqrtDistance < 0.1f)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public Vector3 GetNextWaypoint()
    {
        Ctx.CurrentWaypointIndex = ++Ctx.CurrentWaypointIndex % Ctx.Waypoints.Length;
        return Ctx.Waypoints[Ctx.CurrentWaypointIndex].position;
    }
}