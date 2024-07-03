using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifeyStateMachine : MonoBehaviour
{
    // state variables
    WifeyBaseState currentState;
    WifeyStateFactory states;

    // reference variables
    PlayerStateMachine playerStateMachine;
    GameManager gameManager;
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
    public PlayerStateMachine PlayerStateMachine { get { return playerStateMachine; } }
    public GameManager GameManager { get { return gameManager; } }
    public UIManager UIManager { get { return uIManager; } }
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
        uIManager.WifeyHP.maxValue = uIManager.WifeyHP.value = maxHP;

        // set has reference
        isEatingHash = Animator.StringToHash("isEating");
        isDefendingHash = Animator.StringToHash("isDefending");
        isDyingHash = Animator.StringToHash("isDying");
    }

    void Start()
    {
        HandleHP();
    }

    void Update()
    {
        HandleHP();
        currentState.UpdateStates();
    }
    void HandleHP()
    {
        uIManager.WifeyHP.value--;
        StartCoroutine(TickHP());
    }

    IEnumerator TickHP()
    {
        yield return new WaitForSeconds(1);
        HandleHP();
    }

    void OnTriggerStay(Collider collider)
    {
        currentState.OnTriggerStay(collider);
    }

    // HP increases by 5 for every NPC, receive only 1 dmg per attack
    public void IncreaseHP(int counter)
    {
        uIManager.WifeyHP.value = 5 * counter;
    }
    public void DecreaseHP()
    {
        uIManager.WifeyHP.value--;
    }
}