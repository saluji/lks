using System.Collections.Generic;

// store all states in this script
enum KnightStates
{
    patrol,
    chase,
    death,
    attack,
    eaten
}

// call states from the factory script
public class KnightStateFactory
{
    KnightStateMachine context;

    // store values in a dictionary instead of creating a new instance everytime this script is being called
    Dictionary<KnightStates, KnightBaseState> states = new Dictionary<KnightStates, KnightBaseState>();

    public KnightStateFactory(KnightStateMachine currentContext)
    {
        context = currentContext;
        states[KnightStates.patrol] = new KnightPatrolState(context, this);
        states[KnightStates.chase] = new KnightChaseState(context, this);
        states[KnightStates.death] = new KnightDeathState(context, this);
        states[KnightStates.attack] = new KnightAttackState(context, this);
        states[KnightStates.eaten] = new KnightEatenState(context, this);
    }

    // store states

    public KnightBaseState Patrol()
    {
        return states[KnightStates.patrol];
    }

    public KnightBaseState Chase()
    {
        return states[KnightStates.chase];
    }
    public KnightBaseState Death()
    {
        return states[KnightStates.death];
    }
    public KnightBaseState Attack()
    {
        return states[KnightStates.attack];
    }
    public KnightBaseState Eaten()
    {
        return states[KnightStates.eaten];
    }
}
