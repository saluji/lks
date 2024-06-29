using UnityEngine;

public class NPCChaseState : NPCBaseState
{
    public NPCChaseState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
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
        
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        // chase player
        Ctx.SetDestination(Ctx.Eyes.player.position);
        
        // switch to idle if out of enemy sight
        if (!Ctx.Eyes.IsInRange())
        {
            // switch to idle if player out of NPC detection range
            SwitchState(Factory.Idle());
        }
    }
}