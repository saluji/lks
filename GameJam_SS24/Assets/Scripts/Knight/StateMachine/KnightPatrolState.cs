using UnityEngine;

public class KnightPatrolState : KnightBaseState
{
    public KnightPatrolState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, true);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, 1f);
    }

    public override void UpdateState()
    {
        // chase wifey
        Ctx.SetDestination(Ctx.WifeyStateMachine.transform.position);
        CheckSwitchStates();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Eyes.isDetecting)
        {
            SwitchState(Factory.Chase());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && Ctx.PlayerStateMachine.IsSnatchPressed)
        {
            Ctx.PlayerStateMachine.SnatchCounter++;
            SwitchState(Factory.Eaten());
        }
        else if (collider.gameObject.CompareTag("Fireball"))
        {
            SwitchState(Factory.Death());
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
}