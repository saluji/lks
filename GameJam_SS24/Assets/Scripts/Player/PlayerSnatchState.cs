using UnityEngine;


public class PlayerSnatchState : PlayerBaseState
{
    public PlayerSnatchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Snatch Enter");
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, true);
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        Ctx.AnimationLength = Time.time + 1f;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Snatch Exit");
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, false);
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

    public override void OnTriggerStay(Collider collider)
    {

    }
}