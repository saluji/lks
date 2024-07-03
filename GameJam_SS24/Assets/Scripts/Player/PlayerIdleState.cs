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
        else if (Ctx.IsAttackPressed)
        {
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsSnatchPressed)
        {
            SwitchState(Factory.Snatch());
        }
        else if (Ctx.IsConsumePressed)
        {
            SwitchState(Factory.Consume());
        }
        // else if (Ctx.IsSnatchPressed && Ctx.IsSnatchable && Ctx.ConsumeCounter < 8)
        // else if (Ctx.IsSnatchPressed && Ctx.ConsumeCounter > 0 && Ctx.ConsumeCounter <= 100)
        // else if (Ctx.IsSnatchPressed)
        // {
        //     // Ctx.ConsumeCounter++;
        //     // Ctx.IsSnatchable = true;
        //     SwitchState(Factory.Snatch());
        // }
        // // else if (Ctx.IsConsumePressed && Ctx.ConsumeCounter < 0)
        // else if (Ctx.IsConsumePressed)
        // {
        //     SwitchState(Factory.Consume());
        // }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        // if (collider.gameObject.CompareTag("NPC"))
        {
            // Ctx.UIManager.ShowInteractPanel();
            // // if (Ctx.IsSnatchPressed && Ctx.ConsumeCounter < Ctx.MaxNPC)
            // {
            //     other.gameObject.transform.position = Ctx.JawPosition.position;
            //     // Ctx.ConsumeCounter++;
            //     Ctx.IsSnatchable = true;
            // }
        }
    }
    public override void OnTriggerExit(Collider collider)
    {
        // Ctx.UIManager.HideInteractPanel();
    }
}