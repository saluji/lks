using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Attack: Enter");
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, true);
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
        // Ctx.AnimationLength = Ctx.Animator.GetNextAnimatorClipInfo().length;
        Ctx.AnimationLength = 0;
    }

    public override void UpdateState()
    {
        Ctx.AnimationLength += Time.time;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Attack: Exit");
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, false);
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.AnimationLength > 1)
            SwitchState(Factory.Idle());
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}
