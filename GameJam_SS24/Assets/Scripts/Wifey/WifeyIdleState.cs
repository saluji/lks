using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeyIdleState : WifeyBaseState
{
    public WifeyIdleState(WifeyStateMachine currentContext, WifeyStateFactory wifeyStateFactory) : base(currentContext, wifeyStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsEatingHash, false);
        Ctx.Animator.SetBool(Ctx.IsDefendingHash, false);
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.UIManager.WifeyHP.value < 0)
        {
            SwitchState(Factory.Death());
        }
    }
    public override void OnTriggerStay(Collider collider)
    {
        // if colliding with player and has food 
        if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.SnatchCounter > 0)
        {
            SwitchState(Factory.Eat());
        }
        // if knights near wifey 
        if (collider.gameObject.CompareTag("NPC"))
        {
            SwitchState(Factory.Defend());
        }
    }
}
