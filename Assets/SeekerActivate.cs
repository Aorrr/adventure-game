using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerActivate : StateMachineBehaviour
{
    AudioSource myAudioSource;
    Rigidbody2D rb;
    CamShakeController shaker;

    public AudioClip scream;
    public GameObject ripple;

    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myAudioSource = new AudioSource();
        rb = animator.GetComponent<Rigidbody2D>();
        shaker = FindObjectOfType<CamShakeController>();
        AudioSource.PlayClipAtPoint(scream, rb.position);
        GameObject myRipple = Instantiate(ripple, rb.transform.position, rb.transform.rotation);
        shaker.ShakeIdleAtController(1.5f, 4f, 2f);
        shaker.ShakeRunAtController(1.5f, 4f, 2f);
        Destroy(myRipple, 1.5f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
