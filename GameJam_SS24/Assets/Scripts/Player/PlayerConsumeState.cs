using UnityEngine;

public class PlayerConsumeState : PlayerBaseState
{
    public PlayerConsumeState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.AppliedMovementX = Ctx.AppliedMovementZ = Ctx.TurnSpeed = 0;
        Ctx.Animator.SetBool(Ctx.IsConsumingHash, true);
        Ctx.AnimationLength = Time.time + 2.33f;
        Ctx.IncreaseHP(Ctx.HealAmount);
        Ctx.IsJumpable = false;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.SnatchCounter = 0;
        Ctx.TurnSpeed = 15;
        Ctx.Animator.SetBool(Ctx.IsConsumingHash, false);
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

    public override void OnTriggerStay(Collider collider)
    {
        
    }
}