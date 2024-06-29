using System.Collections.Generic;

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
