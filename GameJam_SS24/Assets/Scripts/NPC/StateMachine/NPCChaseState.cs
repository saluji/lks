using UnityEngine;

public class NPCChaseState : NPCBaseState
{
    public NPCChaseState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, true);

        Ctx.SetDestination(Ctx.TargetPosition);
    }

    public override void UpdateState()
    {
        Ctx.AppliedSpeedX = Ctx.MovementSpeed * Ctx.RunMultiplier;
        Ctx.AppliedSpeedZ = Ctx.MovementSpeed * Ctx.RunMultiplier;

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
        // switch to idle if out of enemy sight
        if (!Ctx.Eyes.IsInRange())
        {
            SwitchState(Factory.Idle());
        }
    }
}