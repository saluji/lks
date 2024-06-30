using UnityEngine;
using UnityEngine.AI;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Idle: Enter");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Idle: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other.CompareTag("NPC"))
        {
            SwitchState(Factory.Death());
        }
    }
}