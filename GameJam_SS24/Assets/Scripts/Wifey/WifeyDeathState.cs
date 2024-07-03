using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeyDeathState : WifeyBaseState
{
    public WifeyDeathState(WifeyStateMachine currentContext, WifeyStateFactory wifeyStateFactory) : base(currentContext, wifeyStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetTrigger(Ctx.IsDyingHash);
        // set game over
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
    public override void OnTriggerEnter(Collider collider)
    {
    }
}
