﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    // Start is called before the first frame update
    bool inRage = false;
    Animator myAnimator;
    CapsuleCollider2D ccollider;


    [SerializeField] int Damage = 1;
    [SerializeField] float attackInterval;
    [SerializeField] int hp = 30;
    private float timeSinceAttack;
    private bool canDamage = true;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        ccollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if (timeSinceAttack > attackInterval)
        {
            canDamage = true;
            hurtPlayer();
        }
    }

    public void hurtPlayer()
    {
        if(ccollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Player>().Hurt(Damage);
            timeSinceAttack = 0;
        }
    }

    public void rage()
    {
        myAnimator.SetBool("inRage", true);
        ccollider.enabled = true;
    }

    public void Hurt(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        //play death animation
        //play particle effect
    }
}