using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerSleep : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    float[] range;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        range = animator.GetComponent<Seeker>().GetMovementRange();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.transform.position.x > range[0] && player.transform.position.x < range[1] && player.transform.position.y < range[2] && player.transform.position.y > range[3])
            animator.SetBool("isActive", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
