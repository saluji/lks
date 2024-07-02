using UnityEngine;

public abstract class NPCParentBaseState
{
    // get variables from ParentStateMachine and set into all state machines
    NPCParentStateMachine ctx;
    NPCParentStateFactory factory;

    protected NPCParentStateMachine Ctx { get { return ctx; } }
    protected NPCParentStateFactory Factory { get { return factory; } }

    public NPCParentBaseState(NPCParentStateMachine currentContext, NPCParentStateFactory nPCParentStateFactory)
    {
        ctx = currentContext;
        factory = nPCParentStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void OnTriggerStay(Collider collider);

    public void UpdateStates()
    {
        UpdateState();
    }

    protected void SwitchState(NPCParentBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        // switch current state of context
        Ctx.CurrentState = newState;
    }
}
