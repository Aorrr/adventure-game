using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HellBeastIdle : StateMachineBehaviour
{
    public float attackRange = 5;

    Transform player;
    HellBeast beast;
    Rigidbody2D rb;

    bool couldShoot;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        beast = animator.GetComponent<HellBeast>();
        couldShoot = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (beast.GetShootCountDown() <= 0 && couldShoot)
        {
            animator.SetTrigger("breath");
            couldShoot = false;
        } 
        else
        {
            animator.SetTrigger("walk");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
