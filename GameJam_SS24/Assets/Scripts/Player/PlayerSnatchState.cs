using UnityEngine;


public class PlayerSnatchState : PlayerBaseState
{
    public PlayerSnatchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Idle: Enter");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        CheckSwitchStates();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Player Idle: Exit");
        Ctx.Animator.SetBool(Ctx.IsSnatchingHash, false);
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        Ctx.StartCoroutine(Ctx.AnimationDuration(1f));
        SwitchState(Factory.Idle());
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}