using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalkState : NPCBaseState
{
    public NPCWalkState(NPCStateMachine currentContext, NPCStateFactory npcStateFactory) : base(currentContext, npcStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsFleeingHash, false);
        Ctx.TargetPosition = GetNextWaypoint();
        Ctx.SetDestination(Ctx.TargetPosition);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, 1f);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        // flee if seeing player
        if (Ctx.Eyes.isDetecting)
        {
            SwitchState(Factory.Flee());
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
        if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }
    }
    public Vector3 GetNextWaypoint()
    {
        Ctx.CurrentWaypointIndex = ++Ctx.CurrentWaypointIndex % Ctx.Waypoints.Length;
        return Ctx.Waypoints[Ctx.CurrentWaypointIndex].position;
    }
}
