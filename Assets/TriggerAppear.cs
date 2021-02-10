using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAppear : MonoBehaviour
{
    bool isTriggered;
    Animator myAnimator;

    private void Start()
    {
        isTriggered = false;
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null && !isTriggered)
        {
            myAnimator.SetTrigger("appear");
            isTriggered = true;
        }
    }
}
