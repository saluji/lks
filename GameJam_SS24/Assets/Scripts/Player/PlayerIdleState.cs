using UnityEngine;

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
        Ctx.IsSnatchable = false;
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
        // else if (Ctx.IsSnatchPressed && Ctx.IsSnatchable && Ctx.ConsumeCounter < 8)
        // {
        //     SwitchState(Factory.Snatch());
        // }
        else if (Ctx.IsSnatchPressed && Ctx.ConsumeCounter < 8)
        {
            Ctx.ConsumeCounter++;
            Ctx.IsSnatchable = true;
        }
        else if (Ctx.IsConsumePressed && Ctx.ConsumeCounter < 8)
        {
            SwitchState(Factory.Consume());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        // GameObject other = collider.gameObject;
        // if (other.CompareTag("NPC"))
        // {
        //     Ctx.UIManager.ShowInteractPanel();
        //     if (Ctx.IsSnatchPressed && Ctx.ConsumeCounter < 8)
        //     {
        //         Ctx.ConsumeCounter++;
        //         Ctx.IsSnatchable = true;
        //     }
        // }
    }
}