using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBeastInactive : StateMachineBehaviour
{
    public float detectRangeX;
    public float detectRangeY;
    Player player;
    HellBeast hellBeast;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>();
        hellBeast = animator.GetComponent<HellBeast>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (System.Math.Abs(player.transform.position.x - hellBeast.transform.position.x) < detectRangeX &&
            System.Math.Abs(player.transform.position.y - hellBeast.transform.position.y) < detectRangeY)
        {
            Debug.Log("DETECTED!!");
            animator.SetTrigger("activate");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
