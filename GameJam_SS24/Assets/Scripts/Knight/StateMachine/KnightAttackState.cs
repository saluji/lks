using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KnightAttackState : KnightBaseState
{
    public KnightAttackState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, true);
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

    public override void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
        }
        else if (collider.gameObject.CompareTag("Player"))
        {
            Ctx.PlayerStateMachine.DecreaseHP();
            Ctx.StartCoroutine(AttackCooldown());
        }
        else if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.IsSnatchPressed)
        {
            Ctx.PlayerStateMachine.SnatchCounter++;
            SwitchState(Factory.Eaten());
        }
        else if (collider.gameObject.CompareTag("Wifey"))
        {
            Ctx.WifeyStateMachine.DecreaseHP();
            SwitchState(Factory.Attack());
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        
    }
    
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(10);
        SwitchState(Factory.Attack());
    }
}