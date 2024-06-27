using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        // low gravity to prevent clipping through ground
        ctx.CurrentMovementY = ctx.GroundedGravity;
        ctx.AppliedMovementY = ctx.GroundedGravity;
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
        if (ctx.IsJumpPressed && !ctx.RequireNewJumpPress)
        {
            SwitchState(factory.Jump());
        }
    }

    public override void InitializeSubState()
    {

    }
}
