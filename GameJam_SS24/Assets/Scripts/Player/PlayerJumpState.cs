using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        HandleJump();
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

    }

    public override void InitializeSubState()
    {

    }

    void HandleJump()
    {
        context.Animator.SetBool(context.IsJumpingHash, true);
        context.IsJumping = true;
        context.CurrentMovementY = context.InitialJumpVelocity;
        context.AppliedMovementY = context.InitialJumpVelocity;
    }
}