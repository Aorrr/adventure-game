using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int damage = 10;
    Animator animator;
    BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void CheckPlayerCollision()
    {
        if (box.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Sanity>().LoseSanity(damage);
        }
    }

    public void StartFire()
    {
        animator.SetTrigger("Fire");
    }
}
