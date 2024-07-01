using UnityEngine;

public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public void HandleGravity()
    {
        Debug.Log("Player Grounded: Enter");
        InitializeSubState();
        Ctx.CurrentMovementY = Ctx.Gravity;
        Ctx.AppliedMovementY = Ctx.Gravity;
    }

    public override void EnterState()
    {
        HandleGravity();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Grounded: Exit");
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            SetSubState(Factory.Run());
        }
        else if (!Ctx.IsMovementPressed && Ctx.IsCrouchPressed)
        {
            SetSubState(Factory.CrouchIdle());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsCrouchPressed)
        {
            SetSubState(Factory.CrouchWalk());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
        }
        else if (!Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Fall());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other.CompareTag("NPC"))
        {
            SwitchState(Factory.Death());
        }
    }
}
