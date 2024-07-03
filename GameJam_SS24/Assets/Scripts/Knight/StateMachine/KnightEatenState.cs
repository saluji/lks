using UnityEngine;

public class KnightEatenState : KnightBaseState
{
    public KnightEatenState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        // Ctx.Animator.SetBool(Ctx.IsEatenHash, false);
        Ctx.SetAgentSpeed(0, 0);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        // Ctx.PlayerStateMachine.IncreaseHP(Ctx.IncreaseHP);
    }

    public override void CheckSwitchStates()
    {
    }

    public override void OnTriggerEnter(Collider collider)
    {
    }
    public override void OnTriggerExit(Collider collider)
    {

    }
    public void Consumed()
    {
        // Ctx.PlayerStateMachine.IncreaseHP(Ctx.IncreaseHP);
        //destroy object
        // Object.Destroy(gameObject);
    }
}
