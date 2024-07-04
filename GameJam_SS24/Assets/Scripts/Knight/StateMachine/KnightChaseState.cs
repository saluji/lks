using UnityEngine;

public class KnightChaseState : KnightBaseState
{
    public KnightChaseState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
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
        // chase player
        Ctx.SetDestination(Ctx.PlayerStateMachine.transform.position);
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.Eyes.IsInRange())
        {
            SwitchState(Factory.Patrol());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SwitchState(Factory.Attack());
        }
        else if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.IsSnatchPressed)
        {
            Ctx.PlayerStateMachine.SnatchCounter++;
            SwitchState(Factory.Eaten());
        }
        else if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }
    }
    public override void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SwitchState(Factory.Patrol());
        }
    }
}