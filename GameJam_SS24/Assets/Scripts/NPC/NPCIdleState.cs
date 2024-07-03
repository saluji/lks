using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCBaseState
{
    public NPCIdleState(NPCStateMachine currentContext, NPCStateFactory npcStateFactory) : base(currentContext, npcStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsFleeingHash, false);
        Ctx.LeaveTime = Time.time + Random.Range(Ctx.MinWaitTime, Ctx.MaxWaitTime);
        Ctx.SetAgentSpeed(0, 0);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
    }
    public override void CheckSwitchStates()
    {
        // flee if player in range
        if (Ctx.Sense.isDetecting)
        {
            SwitchState(Factory.Flee());
        }

        // switch to walk after random amount of time
        if (Time.time > Ctx.LeaveTIme)
        {
            SwitchState(Factory.Walk());
        }

    }
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }
    }
}
