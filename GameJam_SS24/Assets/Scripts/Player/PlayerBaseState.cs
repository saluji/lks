public abstract class PlayerBaseState
{
    PlayerStateMachine ctx;
    PlayerStateFactory factory;
    PlayerBaseState currentSuperState;
    PlayerBaseState currentSubState;
    bool isRootState = false;

    // getter and setter
    protected PlayerStateMachine Ctx { get { return ctx; } }
    protected PlayerStateFactory Factory { get { return factory; } }
    protected bool IsRootState { set { isRootState = value; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        ctx = currentContext;
        factory = playerStateFactory;
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

    // public void ExitStates()
    // {
    //     ExitState();
    //     if (currentSubState != null)
    //     {
    //         currentSubState.ExitStates();
    //     }
    // }

    protected void SwitchState(PlayerBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        if (isRootState)
        {
            // switch current state of context
            ctx.CurrentState = newState;
        }
        else if (currentSuperState != null)
        {
            // set the current super states sub state to the new state
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
