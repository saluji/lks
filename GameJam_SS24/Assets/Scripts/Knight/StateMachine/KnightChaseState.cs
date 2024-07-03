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
        // run towards player
        Ctx.SetDestination(Ctx.Eyes.player.position);
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {

    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SwitchState(Factory.Attack());
        }
        if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }
    }
    public override void OnTriggerExit(Collider collider)
    {

    }
}