public abstract class EnemyBaseState
{
    EnemyStateMachine ctx;
    EnemyStateFactory factory;
    EnemyBaseState currentSuperState;
    EnemyBaseState currentSubState;

    public EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    {
        ctx = currentContext;
        factory = enemyStateFactory;
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

    protected void SwitchState(EnemyBaseState newState)
    {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        // if (isRootState)
        // {
        //     // switch current state of context
        //     ctx.CurrentState = newState;
        // }
        
        if (currentSuperState != null)
        {
            // set the current super states sub state to the new state
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(EnemyBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(EnemyBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}

