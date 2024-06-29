using UnityEngine;

public class NPCIdleState : NPCBaseState
{
    public NPCIdleState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);
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

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        // switch to chase if player in NPCs fov or audible
        if (Ctx.Eyes.isDetecting || Ctx.Ears.isDetecting)
        {
            SwitchState(Factory.Chase());
        }
        
        // switch to patrol after random amount of time
        if (Time.time > Ctx.LeaveTIme)
        {
            SwitchState(Factory.Patrol());
        }
    }
}