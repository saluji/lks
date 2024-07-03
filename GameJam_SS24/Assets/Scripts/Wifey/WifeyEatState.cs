using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeyEatState : WifeyBaseState
{
    public WifeyEatState(WifeyStateMachine currentContext, WifeyStateFactory wifeyStateFactory) : base(currentContext, wifeyStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsEatingHash, true);
        Ctx.Animator.SetBool(Ctx.IsDefendingHash, false);
        Ctx.AnimationLength = Time.time + 1.5f;
        Ctx.IncreaseHP(Ctx.PlayerStateMachine.SnatchCounter);
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {
    }
    public override void CheckSwitchStates()
    {
        if (Time.time > Ctx.AnimationLength)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void OnTriggerStay(Collider collider)
    {
        // if colliding with player and hasFood
        if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.SnatchCounter > 0)
        {
            SwitchState(Factory.Eat());
        }
        if (collider.gameObject.CompareTag("NPC"))
        {
            SwitchState(Factory.Defend());
        }
    }
}
