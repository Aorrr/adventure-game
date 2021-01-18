using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBeastBurntIdle : StateMachineBehaviour
{
    public float attackRange = 5;

    Transform player;
    HellBeast beast;
    Rigidbody2D rb;

    bool couldShoot;
    bool couldFire;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        beast = animator.GetComponent<HellBeast>();
        couldShoot = true;
        couldFire = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (beast.GetShootCountDown() <= 0 && couldShoot)
        {
            Debug.Log(beast.GetRangeCountDown());
            if (beast.GetRangeCountDown() <= 0)
            {
                animator.SetTrigger("range");
                couldShoot = false;
            }
            else
            {
                animator.SetTrigger("breath");
                couldShoot = false;
            }
        }
        else if(System.Math.Abs(player.transform.position.x - beast.transform.position.x) < 1.5f && beast.GetFireCountDown() <= 0 && couldFire) 
        {
            Debug.Log(beast.GetFireCountDown());

            animator.SetTrigger("burnt");
            couldFire = false;
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
