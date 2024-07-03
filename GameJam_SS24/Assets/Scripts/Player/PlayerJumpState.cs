using UnityEngine;

public class PlayerJumpState : PlayerBaseState, IRootState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Player Jump: Enter");
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
        InitializeSubState();
        HandleJump();
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Jump: Exit");
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);

        // replace GetKey with GetKeyDown on jump input
        if (Ctx.IsJumpPressed)
        {
            Ctx.RequireNewJumpPress = true;
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.CharacterController.isGrounded)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.UIManager.PlayerHP.value < 0)
        {
            SwitchState(Factory.Death());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        
    }

    void HandleJump()
    {
        // only jump if not doing any actions on the ground
        if (Ctx.IsJumpable)
        {
            Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
            Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
            Ctx.AppliedMovementY = Ctx.InitialJumpVelocity;
        }
    }

    public void HandleGravity()
    {
        // instantly fall if letting go of jump button
        Ctx.IsFalling = Ctx.CurrentMovementY <= 0.0f || !Ctx.IsJumpPressed;

        // calculate gravity with velocity verlet integration
        if (Ctx.IsFalling)
        {
            // additional gravity applied after reaching the apex of jump
            float previousYVelocity = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY += Ctx.Gravity * Ctx.FallMultiplier * Time.deltaTime;
            Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * 0.5f, Ctx.TerminalVelocity);
        }
        else
        {
            // applied when character not grounded
            float previousYVelocity = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
            Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovementY) * 0.5f;
        }
    }
}