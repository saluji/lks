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
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x / 2;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y / 2;
    }

    public override void UpdateState()
    {
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
        else if(!Ctx.IsMovementPressed  && Ctx.IsCrouchPressed)
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