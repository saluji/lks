using UnityEngine;

public class NPCIdleState : NPCBaseState
{
    public NPCIdleState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory) : base(currentContext, nPCStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Idle: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);
        Ctx.LeaveTime = Time.time + Random.Range(Ctx.MinWaitTime, Ctx.MaxWaitTime);
        Ctx.SetAgentSpeed(0, 0);
    }

    public override void UpdateState()
    {
        Debug.Log("Idle: Update");
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Idle: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Eyes.isDetecting || Ctx.Ears.isDetecting)
        {
            SwitchState(Factory.Chase());
        }
        if (Time.time > Ctx.LeaveTIme)
        {
            SwitchState(Factory.Patrol());
        }
    }
}