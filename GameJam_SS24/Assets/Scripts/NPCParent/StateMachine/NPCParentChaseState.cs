using UnityEngine;

public class NPCParentChaseState : NPCParentBaseState
{
    public NPCParentChaseState(NPCParentStateMachine currentContext, NPCParentStateFactory nPCParentStateFactory) : base(currentContext, nPCParentStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("NPC Chase: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, true);
        Ctx.SetAgentSpeed(Ctx.MovementSpeed, Ctx.RunMultiplier);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("NPC Chase: Exit");
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        // chase player
        Ctx.SetDestination(Ctx.Eyes.player.position);

        // switch to idle if out of enemy sight
        if (!Ctx.Eyes.IsInRange())
        {
            // switch to idle if player out of NPC detection range
            SwitchState(Factory.Idle());
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        // set game over state active and switch to patrol if colliding with player
        GameObject other = collider.gameObject;
        if (other.CompareTag("Player"))
        {
            Ctx.GameOverState = true;
            SwitchState(Factory.Patrol());
        }
    }
}