using UnityEngine;

public abstract class WifeyBaseState
{
    // get variables from ParentStateMachine and set into all state machines
    WifeyStateMachine ctx;
    WifeyStateFactory factory;

    protected WifeyStateMachine Ctx { get { return ctx; } }
    protected WifeyStateFactory Factory { get { return factory; } }

    public WifeyBaseState(WifeyStateMachine currentContext, WifeyStateFactory wifeyStateFactory)
    {
        ctx = currentContext;
        factory = wifeyStateFactory;
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

    protected void SwitchState(WifeyBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        // switch current state of context
        Ctx.CurrentState = newState;
    }
}
