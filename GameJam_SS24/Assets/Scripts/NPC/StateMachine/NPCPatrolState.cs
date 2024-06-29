using UnityEngine;

public class NPCPatrolState : NPCBaseState
{
    public NPCPatrolState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
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

    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        // switch to chase if player detected by NPC
        if (Ctx.Eyes.isDetecting || Ctx.Ears.isDetecting)
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

    public Vector3 GetNextWaypoint()
    {
        Ctx.CurrentWaypointIndex = ++Ctx.CurrentWaypointIndex % Ctx.Waypoints.Length;
        return Ctx.Waypoints[Ctx.CurrentWaypointIndex].position;
    }
}