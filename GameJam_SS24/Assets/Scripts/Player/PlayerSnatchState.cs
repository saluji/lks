using UnityEngine;


public class PlayerSnatchState : PlayerBaseState
{
    public PlayerSnatchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.SnatchCounter++;
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, true);
        Ctx.AnimationLength = Time.time + 1f;
        Ctx.IsJumpable = false;
        Ctx.UIManager.UpdateScore(Ctx.SnatchCounter);
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.MovementSpeed;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.MovementSpeed;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Snatch Exit");
        Ctx.TurnSpeed = 15;
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, false);
        Ctx.IsJumpable = true;
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Time.time > Ctx.AnimationLength)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}