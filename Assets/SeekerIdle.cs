using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerIdle : StateMachineBehaviour
{
    public float attackCountDown;
    public float attackRange = 2.5f;
    public float stopAttackHeight;
    private Player player;
    private Seeker seeker;
    private float[] range;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seeker = animator.GetComponent<Seeker>();
        player = FindObjectOfType<Player>();
        range = seeker.GetMovementRange();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Look at player
        if(player.transform.position.x > seeker.transform.position.x)
        {
            seeker.transform.localScale = new Vector2(1f, 1f);
        } 
        else
        {
            seeker.transform.localScale = new Vector2(-1f, 1f);
        }
        //If player is on higher position
        if(player.transform.position.y >= stopAttackHeight)
        {
            return;
        }
        //Walk
        if (Mathf.Abs(player.transform.position.x - seeker.transform.position.x) > attackRange)
        {
            animator.SetBool("isWalking", true);
        }

        //Attack
        if (Mathf.Abs(player.transform.position.x - seeker.transform.position.x) <= attackRange)
        {
            animator.SetTrigger("attack");
        }

        //Deactivate
        if (player.transform.position.x < range[0] || player.transform.position.x > range[1] || player.transform.position.y > range[2] || player.transform.position.y < range[3])
            animator.SetBool("isActive", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
