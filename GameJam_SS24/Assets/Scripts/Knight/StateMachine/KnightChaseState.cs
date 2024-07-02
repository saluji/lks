using UnityEngine;

public class KnightChaseState : KnightBaseState
{
    public KnightChaseState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Knight Chase: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, true);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, Ctx.RunMultiplier);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Knight Chase: Exit");
    }

    public override void CheckSwitchStates()
    {
        // chase player
        Ctx.SetDestination(Ctx.Eyes.player.position);

        // switch to idle if out of enemy sight
        if (!Ctx.Eyes.IsInRange())
        {
            // switch to idle if player out of Knight detection range
            SwitchState(Factory.Idle());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}