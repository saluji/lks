using UnityEngine;

public class KnightDeathState : KnightBaseState
{
    public KnightDeathState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetTrigger(Ctx.IsDyingHash);
        Ctx.SetAgentSpeed(0, 0);
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {

    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}