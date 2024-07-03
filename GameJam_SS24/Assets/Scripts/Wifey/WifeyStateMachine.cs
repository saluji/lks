using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeyStateMachine : MonoBehaviour
{
    // state variables
    WifeyBaseState currentState;
    WifeyStateFactory states;

    // reference variables
    Animator animator;

    //hash variables
    int isEatingHash;
    int isDefendingHash;
    int isDyingHash;

    float animationLength;
    
    //getter and setter
    public WifeyBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Animator Animator { get { return animator; } }
    public int IsEatingHash { get { return isEatingHash; } }
    public int IsDefendingHash { get { return isDefendingHash; } }
    public int IsDyingHash { get { return isDyingHash; } }
    public float AnimationLength { get { return animationLength; } set { animationLength = value; } }

    void Awake()
    {
        states = new WifeyStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();
    }

    void Update()
    {
        currentState.UpdateStates();
    }
    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(collider);
    }
}
