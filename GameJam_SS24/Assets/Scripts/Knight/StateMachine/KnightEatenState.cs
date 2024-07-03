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
        // Ctx.PlayerStateMachine.IncreaseHP(Ctx.IncreaseHP);
    }

    public override void CheckSwitchStates()
    {
    }

    public override void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.IsSnatchPressed)
        {
            Ctx.PlayerStateMachine.ConsumeCounter++;
            Ctx.PlayerStateMachine.IncreaseHP(Ctx.IncreaseHP);
            SwitchState(Factory.Death());
        }
    }
    public void Consumed()
    {
        //destroy object
        // Object.Destroy(gameObject);
    }
}
