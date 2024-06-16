using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStates : StateMachineBehaviour
{
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    private float nextStateChangeTime;
    int idleIndex = Animator.StringToHash("idleIndex");
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetInteger(idleIndex, 0);
        nextStateChangeTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time > nextStateChangeTime)
        {
            int randomIndex = Random.Range(1, 4);
            animator.SetInteger(idleIndex, randomIndex);

            nextStateChangeTime = Time.time + Random.Range(minWaitTime, maxWaitTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(idleIndex, 0);
    }
}
