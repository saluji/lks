using UnityEngine;

public class KnightEatenState : KnightBaseState
{
    public KnightEatenState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsEatenHash, true);
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
        // regenerate hp if consuming
        if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.IsConsumePressed)
        {
            Ctx.PlayerStateMachine.IncreaseHP();
            Object.Destroy(Ctx.gameObject);
        }
        else if (collider.gameObject.CompareTag("Wifey"))
        {
            Ctx.WifeyStateMachine.IncreaseHP();
            Object.Destroy(Ctx.gameObject);
        }
    }
}
