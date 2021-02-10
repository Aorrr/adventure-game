using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTrigger : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameObject triggerObject;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (triggerObject == null)
            Animate();
    }

    public void Animate()
    {
        animator.SetTrigger("explode");
    }

    public void Explode()
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}
