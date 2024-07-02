using UnityEngine;

public abstract class KnightBaseState
{
    // get variables from ParentStateMachine and set into all state machines
    KnightStateMachine ctx;
    KnightStateFactory factory;

    protected KnightStateMachine Ctx { get { return ctx; } }
    protected KnightStateFactory Factory { get { return factory; } }

    public KnightBaseState(KnightStateMachine currentContext, KnightStateFactory knightStateFactory)
    {
        ctx = currentContext;
        factory = knightStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void OnTriggerEnter(Collider collider);

    public void UpdateStates()
    {
        UpdateState();
    }

    protected void SwitchState(KnightBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        // switch current state of context
        Ctx.CurrentState = newState;
    }
}
