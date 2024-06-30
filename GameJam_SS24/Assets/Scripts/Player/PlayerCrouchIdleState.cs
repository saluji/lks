using UnityEngine;

public class PlayerCrouchIdleState : PlayerBaseState
{
    public PlayerCrouchIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Crouch Idle: Enter");
        Ctx.Animator.SetBool(Ctx.IsCrouchingHash, true);
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Crouch Idle: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            SwitchState(Factory.Walk());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsRunPressed && !Ctx.IsCrouchPressed)
        {
            SwitchState(Factory.Run());
        }
        else if(Ctx.IsMovementPressed && Ctx.IsCrouchPressed)
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