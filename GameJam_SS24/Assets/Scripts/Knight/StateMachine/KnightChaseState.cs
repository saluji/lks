using UnityEngine;

public class KnightChaseState : KnightBaseState
{
    public KnightChaseState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Knight Chase: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, true);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, Ctx.RunMultiplier);
    }

    public override void UpdateState()
    {
        Ctx.SetDestination(Ctx.Eyes.player.position);
    }

    public override void ExitState()
    {
        Debug.Log("Knight Chase: Exit");
    }

    public override void CheckSwitchStates()
    {

    }

    public override void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other.CompareTag("Player"))
        {
            SwitchState(Factory.Attack());
            // SwitchState(Factory.Death());
        }
    }
}