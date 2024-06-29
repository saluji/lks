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
    }

    public override void UpdateState()
    {
        Ctx.AppliedSpeed = Ctx.MovementSpeed;
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
        if (Ctx.Eyes.isDetecting || Ctx.Eyes.isDetecting)
        {
            SwitchState(Factory.Chase());
        }

        // float sqrtDistance = (npcStateMachine.transform.position - _targetPosition).sqrMagnitude;
        // if(sqrtDistance < 0.1f) 
        // {
        //     _targetPosition = GetNextWaypoint();
        //     npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        // }
    }
}