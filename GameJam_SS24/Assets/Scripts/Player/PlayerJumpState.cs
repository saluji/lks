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
        HandleGravity();
    }

    public override void ExitState()
    {
        ctx.Animator.SetBool(ctx.IsJumpingHash, false);
        
        // replace GetKey with GetKeyDown on jump input
        if (ctx.IsJumpPressed)
        {
            ctx.RequireNewJumpPress = true;
        }
    }

    public override void CheckSwitchStates()
    {
        if (ctx.CharacterController.isGrounded)
        {
            SwitchState(factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {

    }

    void HandleJump()
    {
        ctx.Animator.SetBool(ctx.IsJumpingHash, true);
        ctx.IsJumping = true;
        ctx.CurrentMovementY = ctx.InitialJumpVelocity;
        ctx.AppliedMovementY = ctx.InitialJumpVelocity;
    }

    void HandleGravity()
    {
        // instantly fall if letting go of jump button
        ctx.IsFalling = ctx.CurrentMovementY <= 0.0f || !ctx.IsJumpPressed;

        // calculate gravity with velocity verlet integration
        if (ctx.IsFalling)
        {
            // additional gravity applied after reaching the apex of jump
            float previousYVelocity = ctx.CurrentMovementY;
            ctx.CurrentMovementY += ctx.Gravity * ctx.FallMultiplier * Time.deltaTime;
            ctx.AppliedMovementY = Mathf.Max((previousYVelocity + ctx.CurrentMovementY) * 0.5f, ctx.TerminalVelocity);
        }
        else
        {
            // applied when character not grounded
            float previousYVelocity = ctx.CurrentMovementY;
            ctx.CurrentMovementY += ctx.Gravity * Time.deltaTime;
            ctx.AppliedMovementY = (previousYVelocity + ctx.CurrentMovementY) * 0.5f;
        }
    }
}