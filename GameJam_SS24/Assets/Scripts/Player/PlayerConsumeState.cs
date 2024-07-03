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
        Ctx.AnimationLength = Time.time + 1.16f;
        Ctx.IncreaseHP();
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
        else if (Ctx.UIManager.PlayerHP.value < 0)
        {
            SwitchState(Factory.Death());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        
    }
}