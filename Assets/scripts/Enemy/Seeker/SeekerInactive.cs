using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SeekerInactive : StateMachineBehaviour
{
    public float detectRange = 5;
    public AudioClip seekerScream;
    
    CamShakeController shaker;

    Transform player;
    Enemy enemy;
    Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        shaker = FindObjectOfType<CamShakeController>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, rb.position) < detectRange || enemy.GetHealthPercentage() != 1)
        {
            animator.SetBool("isActive", true);
            AudioSource.PlayClipAtPoint(seekerScream, Camera.main.transform.position);
            shaker.ShakeIdleAtController(1.5f, 3f, 2f);
            shaker.ShakeRunAtController(1.5f, 3f, 2f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
