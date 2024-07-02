using System.Collections.Generic;

// store all states in this script
enum ParentStates
{
    idle,
    walk,
    chase
}

// call states from the factory script
public class NPCParentStateFactory
{
    NPCParentStateMachine context;

    // store values in a dictionary instead of creating a new instance everytime this script is being called
    Dictionary<ParentStates, NPCParentBaseState> states = new Dictionary<ParentStates, NPCParentBaseState>();

    public NPCParentStateFactory(NPCParentStateMachine currentContext)
    {
        context = currentContext;
        states[ParentStates.idle] = new ParentIdleState(context, this);
        states[ParentStates.walk] = new NPCParentWalkState(context, this);
        states[ParentStates.chase] = new NPCParentChaseState(context, this);
    }

    // store states
    public NPCParentBaseState Idle()
    {
        return states[ParentStates.idle];
    }

    public NPCParentBaseState Patrol()
    {
        return states[ParentStates.walk];
    }

    public NPCParentBaseState Chase()
    {
        return states[ParentStates.chase];
    }
}
