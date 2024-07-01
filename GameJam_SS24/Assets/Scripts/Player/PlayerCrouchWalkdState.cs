using UnityEngine;

public class PlayerCrouchWalkState : PlayerBaseState
{
    public PlayerCrouchWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Crouch Walk: Enter");
        Ctx.Animator.SetBool(Ctx.IsCrouchingHash, true);
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.MovementSpeed / 2;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.MovementSpeed / 2;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Crouch Walk: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            Ctx.Animator.SetBool(Ctx.IsCrouchingHash, false);
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            Ctx.Animator.SetBool(Ctx.IsCrouchingHash, false);
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            Ctx.Animator.SetBool(Ctx.IsCrouchingHash, false);
            SwitchState(Factory.Run());
        }
        else if (!Ctx.IsMovementPressed && Ctx.IsCrouchPressed)
        {
            SwitchState(Factory.CrouchIdle());
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