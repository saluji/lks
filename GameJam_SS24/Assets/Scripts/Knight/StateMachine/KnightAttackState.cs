using Unity.VisualScripting;
using UnityEngine;

public class KnightAttackState : KnightBaseState
{
    public KnightAttackState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, true);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);
        Ctx.SetAgentSpeed(0, 0);
        Ctx.AnimationLength = Time.time + 0.6f;
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
        // continue attacking
        if (Time.time > Ctx.AnimationLength)
        {
            SwitchState(Factory.Attack());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }

        //decrease hp with every attack
        if (collider.gameObject.CompareTag("Player"))
        {
            Ctx.PlayerStateMachine.DecreaseHP(Ctx.Damage);
        }
        if (collider.gameObject.CompareTag("Wifey"))
        {
            Ctx.WifeyStateMachine.DecreaseHP(Ctx.Damage);
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        // chase if out of player range
        if (collider.gameObject.CompareTag("Player"))
        {
            SwitchState(Factory.Chase());
        }
    }
}