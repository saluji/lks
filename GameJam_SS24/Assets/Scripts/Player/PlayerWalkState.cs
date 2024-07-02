using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Walk: Enter");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        // Ctx.StartCoroutine(Ctx.AudioManager.PlaySFX(Ctx.AudioManager.walk));
        Ctx.IsAudible = true;
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.MovementSpeed;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.MovementSpeed;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Walk: Exit");
        // Ctx.StopCoroutine(Ctx.AudioManager.PlaySFX(Ctx.AudioManager.walk));
        Ctx.IsAudible = false;
        Ctx.IsSnatchable = false;
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsSnatchPressed && Ctx.IsSnatchable)
        {
            SwitchState(Factory.Snatch());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other.CompareTag("NPC"))
        {
            Ctx.UIManager.ShowInteractPanel();
            if (Ctx.IsSnatchPressed && Ctx.ConsumeCounter < 8)
            {
                Ctx.ConsumeCounter++;
                Ctx.IsSnatchable = true;
            }
        }
    }
}