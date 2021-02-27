using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HellBeastWalk : StateMachineBehaviour
{
    public float speed = 6f;

    float boundRight;
    float boundLeft;

    Transform player;
    HellBeast beast;
    Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        beast = animator.GetComponent<HellBeast>();
        boundLeft = beast.GetBounds()[0];
        boundRight = beast.GetBounds()[1];
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        //Look at player
        if (newPos.x - rb.position.x > 0)
        {
            rb.transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            rb.transform.localScale = new Vector2(1f, 1f);
        }

        if (newPos.x > boundRight || newPos.x < boundLeft)
        {
            return;
        }

        rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
