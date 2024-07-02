using UnityEngine;

public class NPCParentWalkState : NPCParentBaseState
{
    public NPCParentWalkState(NPCParentStateMachine currentContext, NPCParentStateFactory npcParentStateFactory) : base(currentContext, npcParentStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Parent Patrol: Enter");
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
        Debug.Log("Parent Patrol: Exit");
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

    public override void OnTriggerStay(Collider collider)
    {

    }

    public Vector3 GetNextWaypoint()
    {
        Ctx.CurrentWaypointIndex = ++Ctx.CurrentWaypointIndex % Ctx.Waypoints.Length;
        return Ctx.Waypoints[Ctx.CurrentWaypointIndex].position;
    }
}