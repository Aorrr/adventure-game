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
    public float rippleStartTime = 0.5f;

    bool haveRippled = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myAudioSource = new AudioSource();
        rb = animator.GetComponent<Rigidbody2D>();
        shaker = FindObjectOfType<CamShakeController>();
        AudioSource.PlayClipAtPoint(scream, rb.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rippleStartTime > 0 && !haveRippled)
        {
            rippleStartTime -= Time.deltaTime;
            if(rippleStartTime <= 0)
            {
                haveRippled = true;
                GameObject myRipple = Instantiate(ripple, rb.transform.position, rb.transform.rotation);
                Destroy(myRipple, 1.5f);
                shaker.ShakeIdleAtController(1.5f, 4f, 2f);
                shaker.ShakeRunAtController(1.5f, 4f, 2f);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
