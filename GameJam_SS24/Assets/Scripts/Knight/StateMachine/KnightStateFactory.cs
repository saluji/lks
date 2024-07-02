using System.Collections.Generic;

// store all states in this script
enum KnighttStates
{
    idle,
    patrol,
    chase,
    death,
    attack
}

// call states from the factory script
public class KnightStateFactory
{
    KnightStateMachine context;

    // store values in a dictionary instead of creating a new instance everytime this script is being called
    Dictionary<KnighttStates, KnightBaseState> states = new Dictionary<KnighttStates, KnightBaseState>();

    public KnightStateFactory(KnightStateMachine currentContext)
    {
        context = currentContext;
        states[KnighttStates.idle] = new KnightIdleState(context, this);
        states[KnighttStates.patrol] = new KnightPatrolState(context, this);
        states[KnighttStates.chase] = new KnightChaseState(context, this);
        states[KnighttStates.death] = new KnightDeathState(context, this);
        states[KnighttStates.attack] = new KnightAttackState(context, this);
    }

    // store states
    public KnightBaseState Idle()
    {
        return states[KnighttStates.idle];
    }

    public KnightBaseState Patrol()
    {
        return states[KnighttStates.patrol];
    }

    public KnightBaseState Chase()
    {
        return states[KnighttStates.chase];
    }
    public KnightBaseState Death()
    {
        return states[KnighttStates.death];
    }
    public KnightBaseState Attack()
    {
        return states[KnighttStates.attack];
    }
}
