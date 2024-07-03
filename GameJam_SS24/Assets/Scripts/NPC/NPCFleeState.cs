using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFleeState : NPCBaseState
{
    public NPCFleeState(NPCStateMachine currentContext, NPCStateFactory npcStateFactory) : base(currentContext, npcStateFactory)
    {

    }
    public override void EnterState() { }
    public override void UpdateState() { }
    public override void ExitState() { }
    public override void CheckSwitchStates() { }
    public override void OnTriggerEnter(Collider collider) { }
}
