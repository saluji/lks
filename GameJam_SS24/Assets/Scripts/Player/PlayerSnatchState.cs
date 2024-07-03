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
        Ctx.AnimationLength = Time.time + 0.5f;
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
        else if (Ctx.UIManager.PlayerHP.value < 0)
        {
            SwitchState(Factory.Death());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}