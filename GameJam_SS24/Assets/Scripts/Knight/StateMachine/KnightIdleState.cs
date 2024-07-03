using UnityEngine;

public class KnightIdleState : KnightBaseState
{
    public KnightIdleState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
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

    public override void CheckSwitchStates()
    {
        // // switch to chase if player in range
        if (Ctx.Sense.isDetecting)
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
        if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }
    }
    public override void OnTriggerExit(Collider collider)
    {

    }
}
