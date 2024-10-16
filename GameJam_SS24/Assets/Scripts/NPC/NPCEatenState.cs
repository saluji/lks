
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCEatenState : NPCBaseState
{
    public NPCEatenState(NPCStateMachine currentContext, NPCStateFactory npcStateFactory) : base(currentContext, npcStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsEatenHash, true);
        Ctx.SetAgentSpeed(0, 0);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        Ctx.PlayerStateMachine.IncreaseHP();
    }
    public override void CheckSwitchStates()
    {

    }
    public override void OnTriggerStay(Collider collider)
    {

    }
    public void Consumed()
    {
        //destroy object
        // Object.Destroy(gameObject);
    }
}
