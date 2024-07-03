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
    UIManager uIManager;

    // wifey stats
    [SerializeField] int maxHP = 200;

    //hash variables
    int isEatingHash;
    int isDefendingHash;
    int isDyingHash;

    float animationLength;

    // getter and setter
    public WifeyBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Animator Animator { get { return animator; } }
    // public UIManager UIManager { get { return uIManager; } }
    public int IsEatingHash { get { return isEatingHash; } }
    public int IsDefendingHash { get { return isDefendingHash; } }
    public int IsDyingHash { get { return isDyingHash; } }
    public float AnimationLength { get { return animationLength; } set { animationLength = value; } }

    void Awake()
    {
        // initial state 
        states = new WifeyStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();

        // set max HP value
        uIManager.WifeyHP.maxValue = maxHP;
        uIManager.WifeyHP.value = maxHP;

        // set has reference
        isEatingHash = Animator.StringToHash("isEating");
        isDefendingHash = Animator.StringToHash("isDefending");
        isDyingHash = Animator.StringToHash("isDying");
    }

    void Update()
    {
        currentState.UpdateStates();
    }
    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(collider);
    }
    public void IncreaseHP(int amount)
    {
        uIManager.WifeyHP.value += amount;
    }
    public void DecreaseHP(int amount)
    {
        uIManager.WifeyHP.value -= amount;
    }
}