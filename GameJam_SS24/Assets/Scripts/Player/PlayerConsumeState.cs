using UnityEngine;

public class PlayerConsumeState : PlayerBaseState
{
    public PlayerConsumeState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Consume Enter");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.Animator.SetBool(Ctx.IsConsumingHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        CheckSwitchStates();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Consume Exit");
        Ctx.ConsumeCounter = 0;
        Ctx.Animator.SetBool(Ctx.IsConsumingHash, false);
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        // Ctx.StartCoroutine(Ctx.AnimationDuration(2.4f));
        // SwitchState(Factory.Idle());
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}