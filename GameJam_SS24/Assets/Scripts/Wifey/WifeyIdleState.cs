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
        // if hp = 0 => Death
    }
    public override void OnTriggerEnter(Collider collider)
    {
        // if colliding with player and hasFood
        if (collider.gameObject.CompareTag("Player"))
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
