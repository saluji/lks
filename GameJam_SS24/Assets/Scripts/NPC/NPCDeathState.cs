using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeathState : NPCBaseState
{
    public NPCDeathState(NPCStateMachine currentContext, NPCStateFactory npcStateFactory) : base(currentContext, npcStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetTrigger(Ctx.IsDyingHash);
        Ctx.SetAgentSpeed(0, 0);
    }
    public override void UpdateState() { }
    public override void ExitState() { }
    public override void CheckSwitchStates() { }
    public override void OnTriggerStay(Collider collider) { }
}
