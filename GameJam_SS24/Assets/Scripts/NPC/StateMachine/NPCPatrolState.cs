using Unity.VisualScripting;
using UnityEngine;

public class NPCPatrolState : NPCBaseState
{
    public NPCPatrolState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Patrol: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, true);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);

        // if (Ctx.TargetPosition == Vector3.zero)
        // {
        //     Ctx.TargetPosition = Ctx.Waypoints[0].position;
        // }
        Ctx.TargetPosition = GetNextWaypoint();
        Ctx.SetDestination(Ctx.TargetPosition);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, 1f);
    }

    public override void UpdateState()
    {
        Debug.Log("Patrol: Update");
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Patrol: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Eyes.isDetecting || Ctx.Eyes.isDetecting)
        {
            SwitchState(Factory.Chase());
        }

        float sqrtDistance = (Ctx.transform.position - Ctx.TargetPosition).sqrMagnitude;
        if (sqrtDistance < 0.1f)
        {
            // Ctx.TargetPosition = GetNextWaypoint();
            SwitchState(Factory.Idle());
        }
    }

    public Vector3 GetNextWaypoint()
    {
        Ctx.CurrentWaypointIndex = ++Ctx.CurrentWaypointIndex % Ctx.Waypoints.Length;
        return Ctx.Waypoints[Ctx.CurrentWaypointIndex].position;
    }
}