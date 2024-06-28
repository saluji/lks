using System;
using UnityEngine;

[Serializable]
public class NPCPatrolState : BaseState
{
    public Transform[] Waypoints;

    private int _currentWaypointIndex;

    private Vector3 _targetPosition;

    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnEnterState");
        NPCStateMachine npcStateMachine = controller as NPCStateMachine;

        if(_targetPosition == Vector3.zero) 
        {
            _targetPosition = Waypoints[0].position;
        }

        npcStateMachine.SetDestination(_targetPosition);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnUpdateState");

        NPCStateMachine npcStateMachine = controller as NPCStateMachine;

        // Transitions
        // NPC reached waypoint? > Switch to idle
        float sqrtDistance = (npcStateMachine.transform.position - _targetPosition).sqrMagnitude;
        if(sqrtDistance < 0.1f) 
        {
            _targetPosition = GetNextWaypoint();
            npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        }

        // Transitions
        // Can see player > Switch to flee
        if (npcStateMachine.CanSeePlayer || npcStateMachine.CanHearPlayer)
        {
            npcStateMachine.SwitchToState(npcStateMachine.HideState);
        }
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnExitState");
    }

    public Vector3 GetNextWaypoint() 
    {
        _currentWaypointIndex = ++_currentWaypointIndex % Waypoints.Length;
        return Waypoints[_currentWaypointIndex].position;
    }
}
