using UnityEngine;

public class PlayerDeathState : PlayerBaseState, IRootState
{
    public PlayerDeathState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Player Death: Enter");
        Ctx.Animator.SetTrigger(Ctx.IsDyingHash);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        Ctx.TurnSpeed = 0;
        Ctx.StartCoroutine(Ctx.GameManager.GameOverCountdown());
    }

    public override void UpdateState()
    {
        HandleGravity();
    }

    public override void ExitState()
    {

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

    public void HandleGravity()
    {   
        float previousYVelocity = Ctx.CurrentMovementY;
        Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
        Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovementY) * 0.5f;
    }
}
