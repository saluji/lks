using UnityEngine;

public class NPCPatrolState : NPCBaseState
{
    public NPCPatrolState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("NPC Patrol: Enter");
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
        Debug.Log("NPC Patrol: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        // as long as game over is not active
        if ((Ctx.Eyes.isDetecting || Ctx.Ears.isDetecting) && !Ctx.GameOverState)
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