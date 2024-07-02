using UnityEngine;

public class PlayerStompState : PlayerBaseState, IRootState
{
    public PlayerStompState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Debug.Log("Consume Enter");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.Animator.SetBool(Ctx.IsStompingHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        Ctx.TurnSpeed = 0;
        // Ctx.AnimationLength = Time.time + 0.5f;
        // Ctx.AnimationLength = Time.time + 2f;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Consume Exit");
        Ctx.TurnSpeed = 15;
        Ctx.Animator.SetBool(Ctx.IsStompingHash, false);
    }

    public override void InitializeSubState()
    {
        if (Ctx.CharacterController.isGrounded)
        {
            SetSubState(Factory.Idle());
        }
    }

    public override void CheckSwitchStates()
    {
        // if (Time.time > Ctx.AnimationLength)
        // {
        //     SwitchState(Factory.Idle());
        // }
        if (Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
    public void HandleGravity()
    {
        float previousYVelocity = Ctx.CurrentMovementY;
        Ctx.CurrentMovementY += Ctx.Gravity * Time.deltaTime;
        // Ctx.AppliedMovementY = (previousYVelocity + Ctx.CurrentMovementY) * 0.5f;
        Ctx.AppliedMovementY = previousYVelocity + Ctx.CurrentMovementY;
    }
}