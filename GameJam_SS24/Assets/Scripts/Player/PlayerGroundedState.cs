using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {

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
        // if player is grounded and jump is pressed, switch to jump state
        if (context.IsJumpPressed)
        {
            SwitchState(factory.Jump());
        }
    }

    public override void InitializeSubState()
    {

    }
}
