using System.Collections.Generic;

// store all states in this script
enum WifeyStates
{
    idle,
    eat,
    defend,
    death
}

// call states from the factory script
public class WifeyStateFactory
{
    WifeyStateMachine context;

    // store values in a dictionary instead of creating a new instance everytime this script is being called
    Dictionary<WifeyStates, WifeyBaseState> states = new Dictionary<WifeyStates, WifeyBaseState>();

    public WifeyStateFactory(WifeyStateMachine currentContext)
    {
        context = currentContext;
        states[WifeyStates.idle] = new WifeyIdleState(context, this);
        states[WifeyStates.eat] = new WifeyEatState(context, this);
        states[WifeyStates.defend] = new WifeyDefendState(context, this);
        states[WifeyStates.death] = new WifeyDeathState(context, this);
    }

    // store states
    public WifeyBaseState Idle()
    {
        return states[WifeyStates.idle];
    }

    public WifeyBaseState Eat()
    {
        return states[WifeyStates.eat];
    }

    public WifeyBaseState Defend()
    {
        return states[WifeyStates.defend];
    }
    public WifeyBaseState Death()
    {
        return states[WifeyStates.death];
    }
}