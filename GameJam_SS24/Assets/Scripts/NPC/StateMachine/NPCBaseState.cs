public abstract class NPCBaseState
{
    // get variables from NPCStateMachine and set into all state machines
    NPCStateMachine ctx;
    NPCStateFactory factory;
    NPCBaseState currentSuperState;
    NPCBaseState currentSubState;

    protected NPCStateMachine Ctx { get { return ctx; } }
    protected NPCStateFactory Factory { get { return factory; } }

    public NPCBaseState(NPCStateMachine currentContext, NPCStateFactory nPCStateFactory)
    {
        ctx = currentContext;
        factory = nPCStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void InitializeSubState();
    public abstract void CheckSwitchStates();

    public void UpdateStates()
    {
        UpdateState();
        if (currentSubState != null)
        {
            currentSubState.UpdateStates();
        }
    }

    public void ExitStates()
    {
        ExitState();
        if (currentSubState != null)
        {
            currentSubState.ExitStates();
        }
    }

    protected void SwitchState(NPCBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        if (currentSuperState != null)
        {
            // set the current super states sub state to the new state
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(NPCBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(NPCBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}