using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerWalk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 2.5f;
    float[] range;
    public float walkDuration = 2f;

    Transform player;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        range = animator.GetComponent<Seeker>().GetMovementRange();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        if (player.transform.position.x < range[0] || player.transform.position.x > range[1] || player.transform.position.y > range[2] || player.transform.position.y < range[3])
            animator.SetBool("isActive", false);

        if (newPos.x - rb.position.x > 0)
        {
            rb.transform.localScale = new Vector2(1f, 1f);
            //if (rb.transform.position.x > 12.25f)
                //return;
        }   
        else
        {
            rb.transform.localScale = new Vector2(-1f, 1f);
            //if (rb.transform.position.x < 6.9f)
                //return;
        }
        
        if(newPos.x > range[0] && newPos.x < range[1])
        {
            rb.MovePosition(newPos);
        }
            

        if(Mathf.Abs(player.transform.position.x - rb.transform.position.x) <= attackRange)
        {
            animator.SetBool("isWalking", false);
        }

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
