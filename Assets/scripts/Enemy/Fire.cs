using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int damage = 10;
    [SerializeField] float frostDuration = 3;
    Animator animator;
    BoxCollider2D box;
    CircleCollider2D circle;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void CheckPlayerCollision()
    {
        if (box.IsTouchingLayers(LayerMask.GetMask("Player")) ||
            circle.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Sanity>().LoseSanity(damage);
            FindObjectOfType<Player>().SlowDown(0.6f, 3);
        }
    }

    public void StartFire()
    {
        animator.SetTrigger("Fire");
    }
}
