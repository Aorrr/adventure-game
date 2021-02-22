using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int damage = 10;
    [SerializeField] int frostDuration = 3;
    [SerializeField] float slowFactor = 0.5f;

    Animator animator;
    BoxCollider2D box;
    CircleCollider2D circle;
    Player player;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    public void CheckPlayerCollision()
    {
        if (box.IsTouchingLayers(LayerMask.GetMask("Player")) ||
            circle.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if(!player.CouldHurt()) { return; }
            FindObjectOfType<Player>().Hurt(damage, "magical");
            FindObjectOfType<Player>().SlowDown(slowFactor, frostDuration);
        }
    }

    public void StartFire()
    {

        animator.SetTrigger("Fire");
    }
}
