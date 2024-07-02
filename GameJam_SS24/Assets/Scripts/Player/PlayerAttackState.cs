using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Death: Enter");
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        Ctx.StartCoroutine(Ctx.GameManager.GameOverCountdown());
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, false);
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {

    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}
