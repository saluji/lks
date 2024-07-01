using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Run: Enter");
        Ctx.Animator.SetBool(Ctx.IsCrouchingHash, false);
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
        Ctx.IsAudible = true;
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.MovementSpeed * Ctx.RunMultiplier;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.MovementSpeed * Ctx.RunMultiplier;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Run: Exit");
        Ctx.IsAudible = false;
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsCrouchPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsCrouchPressed)
        {
            SwitchState(Factory.CrouchWalk());
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