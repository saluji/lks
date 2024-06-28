using System.Collections.Generic;

enum EnemyStates
{
    idle,
    patrol,
    chase,
}

// call states from the factory script
public class EnemyStateFactory
{
    EnemyStateMachine context;

    // store values in a dictionary instead of creating a new instance everytime this script is being called
    Dictionary<EnemyStates, EnemyBaseState> states = new Dictionary<EnemyStates, EnemyBaseState>();

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        context = currentContext;
        states[EnemyStates.idle] = new EnemyIdleState(context, this);
        states[EnemyStates.patrol] = new EnemyPatrolState(context, this);
        states[EnemyStates.chase] = new EnemyChaseState(context, this);
    }

    public EnemyBaseState Idle()
    {
        return states[EnemyStates.idle];
    }

    public EnemyBaseState Patrol()
    {
        return states[EnemyStates.patrol];
    }

    public EnemyBaseState Chase()
    {
        return states[EnemyStates.chase];
    }
}
