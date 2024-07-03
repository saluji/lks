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
        Ctx.AppliedMovementX = Ctx.AppliedMovementZ = Ctx.TurnSpeed = 0;
        Debug.Log("Consume Enter");
        Ctx.Animator.SetBool(Ctx.IsStompingHash, true);
        // Ctx.AnimationLength = Time.time + 0.66f;
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

    public override void OnTriggerEnter(Collider collider)
    {

    }
    public override void OnTriggerExit(Collider collider)
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