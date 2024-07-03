using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeyDefendState : WifeyBaseState
{
    public WifeyDefendState(WifeyStateMachine currentContext, WifeyStateFactory wifeyStateFactory) : base(currentContext, wifeyStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsEatingHash, false);
        Ctx.Animator.SetBool(Ctx.IsDefendingHash, true);
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

        // if no more enemies in range > idle
    }
    public override void OnTriggerEnter(Collider collider)
    {
        // if colliding with player and hasFood
        if (collider.gameObject.CompareTag("Player"))
        {
            SwitchState(Factory.Eat());
        }
        if (collider.gameObject.CompareTag("NPC"))
        {
            SwitchState(Factory.Defend());
        }
    }
}
