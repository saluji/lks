using UnityEngine;

public abstract class NPCParentBaseState
{
    // get variables from ParentStateMachine and set into all state machines
    NPCParentStateMachine ctx;
    NPCParentStateFactory factory;
    // ParentBaseState currentSuperState;
    // ParentBaseState currentSubState;

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
    public abstract void InitializeSubState();
    public abstract void CheckSwitchStates();
    public abstract void OnTriggerEnter(Collider collider);

    public void UpdateStates()
    {
        UpdateState();
        // if (currentSubState != null)
        // {
        //     currentSubState.UpdateStates();
        // }
    }

    protected void SwitchState(NPCParentBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        // switch current state of context
        Ctx.CurrentState = newState;

        // if (currentSuperState != null)
        // {
        //     // set the current super states sub state to the new state
        //     currentSuperState.SetSubState(newState);
        // }
    }

    // protected void SetSuperState(ParentBaseState newSuperState)
    // {
    //     currentSuperState = newSuperState;
    // }

    // protected void SetSubState(ParentBaseState newSubState)
    // {
    //     currentSubState = newSubState;
    //     newSubState.SetSuperState(this);
    // }
}
