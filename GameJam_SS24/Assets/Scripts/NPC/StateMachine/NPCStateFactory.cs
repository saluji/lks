using System.Collections.Generic;

// store all states in this script
enum NPCStates
{
    idle,
    patrol,
    chase
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
        states[NPCStates.patrol] = new NPCPatrolState(context, this);
        states[NPCStates.chase] = new NPCChaseState(context, this);
    }

    // store states
    public NPCBaseState Idle()
    {
        return states[NPCStates.idle];
    }

    public NPCBaseState Patrol()
    {
        return states[NPCStates.patrol];
    }

    public NPCBaseState Chase()
    {
        return states[NPCStates.chase];
    }
}
