using System.Diagnostics;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.AppliedMovementX = Ctx.AppliedMovementZ = 0;
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsConsumePressed && Ctx.SnatchCounter > 0)
        {
            SwitchState(Factory.Consume());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("NPC") && Ctx.IsSnatchPressed && Ctx.SnatchCounter < Ctx.MaxNPC)
        {
            collider.transform.position = Ctx.transform.position;
            collider.transform.SetParent(Ctx.Mouth);
            SwitchState(Factory.Snatch());
        }
    }
}