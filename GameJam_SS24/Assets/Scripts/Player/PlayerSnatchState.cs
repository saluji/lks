using UnityEngine;


public class PlayerSnatchState : PlayerBaseState
{
    public PlayerSnatchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.AppliedMovementX = Ctx.AppliedMovementZ = Ctx.TurnSpeed = 0;
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, true);
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AnimationLength = Time.time + 1f;
        Ctx.IsJumpable = false;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Snatch Exit");
        Ctx.TurnSpeed = 15;
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, false);
        Ctx.IsJumpable = true;
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Time.time > Ctx.AnimationLength)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }
    public override void OnTriggerExit(Collider collider)
    {

    }
}