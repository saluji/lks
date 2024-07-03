using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFleeState : NPCBaseState
{
    public NPCFleeState(NPCStateMachine currentContext, NPCStateFactory npcStateFactory) : base(currentContext, npcStateFactory)
    {

    }
    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsFleeingHash, true);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, Ctx.RunMultiplier);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        // flee towards player's opposite direction
        Ctx.SetDestination(Ctx.transform.position - Ctx.PlayerStateMachine.transform.position);
        // if outside player range
        if (!Ctx.Eyes.IsInRange())
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }
    }
}