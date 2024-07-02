using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Player Run: Enter");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
        // Ctx.StartCoroutine(Ctx.AudioManager.PlaySFX(Ctx.AudioManager.run));
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.MovementSpeed * Ctx.RunMultiplier;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.MovementSpeed * Ctx.RunMultiplier;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("Player Run: Exit");
        // Ctx.StopCoroutine(Ctx.AudioManager.PlaySFX(Ctx.AudioManager.run));
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
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SwitchState(Factory.Walk());
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