using UnityEngine;

public abstract class NPCBaseState
{
    // get variables from ParentStateMachine and set into all state machines
    NPCStateMachine ctx;
    NPCStateFactory factory;

    protected NPCStateMachine Ctx { get { return ctx; } }
    protected NPCStateFactory Factory { get { return factory; } }

    public NPCBaseState(NPCStateMachine currentContext, NPCStateFactory npcStateFactory)
    {
        ctx = currentContext;
        factory = npcStateFactory;
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

    protected void SwitchState(NPCBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        // switch current state of context
        Ctx.CurrentState = newState;
    }
}
