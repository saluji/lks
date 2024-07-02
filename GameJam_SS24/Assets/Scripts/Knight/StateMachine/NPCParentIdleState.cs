using UnityEngine;

public class ParentIdleState : NPCParentBaseState
{
    public ParentIdleState(NPCParentStateMachine currentContext, NPCParentStateFactory npcParentStateFactory) : base(currentContext, npcParentStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("NPC Idle: Enter");
        Ctx.Animator.SetBool(Ctx.IsPatrolingHash, false);
        Ctx.Animator.SetBool(Ctx.IsChasingHash, false);
        Ctx.LeaveTime = Time.time + Random.Range(Ctx.MinWaitTime, Ctx.MaxWaitTime);
        Ctx.SetAgentSpeed(0, 0);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("NPC Idle: Exit");
    }

    public override void CheckSwitchStates()
    {
        // switch to chase if player in NPCs fov or audible as long as game over is not active
        if ((Ctx.Eyes.isDetecting || Ctx.Ears.isDetecting) && !Ctx.GameOverState)
        {
            SwitchState(Factory.Chase());
        }

        // switch to patrol after random amount of time
        if (Time.time > Ctx.LeaveTIme)
        {
            SwitchState(Factory.Patrol());
        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }
}