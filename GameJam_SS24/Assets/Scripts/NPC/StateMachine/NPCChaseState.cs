using UnityEngine;

public class NPCChaseState : NPCBaseState
{
    public NPCChaseState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Chase: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, true);

        Ctx.SetAgentSpeed(Ctx.MovementSpeed, Ctx.RunMultiplier);
    }

    public override void UpdateState()
    {
        Debug.Log("Chase: Update");
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Chase: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        Ctx.SetDestination(Ctx.Eyes.player.position);
        // Ctx.SetAgentSpeed(Ctx.MovementSpeed, Ctx.RunMultiplier);
        // switch to idle if out of enemy sight
        if (!Ctx.Eyes.IsInRange())
        {
            // Ctx.TargetPosition = GetNearestWaypoint(Ctx.transform.position);
            SwitchState(Factory.Idle());
        }
    }

    // public Vector3 GetNearestWaypoint(Vector3 position)
    // {
    //     if (Ctx.Waypoints.Length < 2)
    //         return Vector3.zero;

    //     int shortestSqrtDistanceIndex = 0;
    //     float shortestSqrtDistance = (Ctx.Waypoints[0].position - position).sqrMagnitude;
    //     for (int i = 1; i < Ctx.Waypoints.Length; i++)
    //     {
    //         float sqrtDistance = (Ctx.Waypoints[i].position - position).sqrMagnitude;
    //         if (sqrtDistance < shortestSqrtDistance)
    //         {
    //             shortestSqrtDistance = sqrtDistance;
    //             shortestSqrtDistanceIndex = i;
    //         }
    //     }
    //     return Ctx.Waypoints[shortestSqrtDistanceIndex].position;
    // }
}