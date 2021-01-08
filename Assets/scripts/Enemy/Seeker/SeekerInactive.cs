using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SeekerInactive : StateMachineBehaviour
{
    public float detectRange = 5;

    Transform player;
    Enemy enemy;
    Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) < detectRange)
        {
            animator.SetBool("isActive", true);
        }
        else if(enemy.GetHealthPercentage() != 1)
        {
            animator.SetBool("isActive", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
