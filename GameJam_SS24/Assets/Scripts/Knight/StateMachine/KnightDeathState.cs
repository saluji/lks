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
        Ctx.AnimationLength = Time.time + 2f;
    }

    public override void UpdateState()
    {
        if (Time.time > Ctx.AnimationLength)
        {
            Object.Destroy(Ctx.gameObject);
        }
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
    public override void OnTriggerExit(Collider collider)
    {

    }
}