using System.Collections.Generic;

// store all states in this script
enum NPCStates
{
    idle,
    walk,
    flee,
    death,
    eaten
}

// call states from the factory script
public class NPCStateFactory
{
    NPCStateMachine context;

    // store values in a dictionary instead of creating a new instance everytime this script is being called
    Dictionary<NPCStates, NPCBaseState> states = new Dictionary<NPCStates, NPCBaseState>();

    public NPCStateFactory(NPCStateMachine currentContext)
    {
        context = currentContext;
        states[NPCStates.idle] = new NPCIdleState(context, this);
        states[NPCStates.walk] = new NPCWalkState(context, this);
        states[NPCStates.flee] = new NPCFleeState(context, this);
        states[NPCStates.death] = new NPCDeathState(context, this);
        states[NPCStates.eaten] = new NPCEatenState(context, this);
    }

    // store states
    public NPCBaseState Idle()
    {
        return states[NPCStates.idle];
    }

    public NPCBaseState Walk()
    {
        return states[NPCStates.walk];
    }

    public NPCBaseState Flee()
    {
        return states[NPCStates.flee];
    }
    public NPCBaseState Death()
    {
        return states[NPCStates.death];
    }
    public NPCBaseState Eaten()
    {
        return states[NPCStates.eaten];
    }
}
