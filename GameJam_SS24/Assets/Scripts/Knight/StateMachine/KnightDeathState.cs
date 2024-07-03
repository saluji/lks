using UnityEngine;

public class KnightDeathState : KnightBaseState
{
    public KnightDeathState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory) : base(currentContext, knightStateFactory)
    {

    }

    public override void EnterState()
    {
        Ctx.Animator.SetTrigger(Ctx.IsDyingHash);
        Ctx.SetAgentSpeed(0, 0);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {
        // as long as game over is not active
        if (Ctx.Eyes.isDetecting)
        {
            SwitchState(Factory.Chase());
        }

        // switch to idle if reaching waypoint
        float sqrtDistance = (Ctx.transform.position - Ctx.TargetPosition).sqrMagnitude;
        if (sqrtDistance < 0.1f)
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }
    public override void OnTriggerExit(Collider collider)
    {

    }
}