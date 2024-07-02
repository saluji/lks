using UnityEngine;

public class KnightIdleState : KnightBaseState
{
    public KnightIdleState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Knight Idle: Enter");
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
        Debug.Log("Knight Idle: Exit");
    }

    public override void CheckSwitchStates()
    {
        // switch to chase if player in range
        if (Ctx.Sense.isDetecting && !Ctx.GameOverState)
        {
            SwitchState(Factory.Chase());
        }

        // switch to patrol after random amount of time
        if (Time.time > Ctx.LeaveTIme)
        {
            SwitchState(Factory.Patrol());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }
}