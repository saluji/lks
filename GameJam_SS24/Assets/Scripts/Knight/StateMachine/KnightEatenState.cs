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
        // snatching makes player slower and increases consumption counter
        if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.IsSnatchPressed)
        {
            Ctx.PlayerStateMachine.SnatchCounter++;
        }
        // regenerate hp if consuming
        if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.IsConsumePressed)
        {
            Ctx.PlayerStateMachine.IncreaseHP(Ctx.IncreaseHP);
            // Object.Destroy(gameObject.this);
        }
    }
    public void Consumed()
    {
        //destroy object
        // Object.Destroy(gameObject);
    }
}
