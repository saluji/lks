using System;
using UnityEngine;

[Serializable]
public class NPCFleeState : BaseState
{
    public float FleeDistance;

    private Vector3 _fleePosition;

    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnEnterState");
        NPCStateMachine npcStateMachine = controller as NPCStateMachine;

        // Calculate the position to which you want to escape
        _fleePosition = (npcStateMachine.transform.position - npcStateMachine.PlayerPosition).normalized * FleeDistance
            + npcStateMachine.transform.position;

        // Move to the calculated position
        npcStateMachine.SetDestination(_fleePosition);
        npcStateMachine.SetAgentSpeedMultiplier(2.5f);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnUpdateState");
        NPCStateMachine npcStateMachine = controller as NPCStateMachine;


        // Reached the save spot? > Switch to idle
        if ((npcStateMachine.transform.position - _fleePosition).sqrMagnitude < 3f) 
        {
            npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        }
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnExitState");
        NPCStateMachine npcStateMachine = controller as NPCStateMachine;

        npcStateMachine.SetAgentSpeedMultiplier(1f);
    }
}
