﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    float attackCD = 3.5f;
    float attackTimer = 1f;
    Player player;
    Animator amtr;
    EnemyBody body;
    EnemyMovement movement;

    [SerializeField] int DefenceStrength = 300;
    bool defence = false;
    bool detected = false;

    int idleDuration = 2;
    float idleTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        amtr = GetComponent<Animator>();
        body = GetComponentInChildren<EnemyBody>();
        PrepareStats();
        movement = GetComponentInChildren<EnemyMovement>();
    }


    // Update is called once per frame
    void Update()
    {
        if(!detected) { return; }

        if(!CouldDamage())
        {
            attackTimer += Time.deltaTime;
        }

        if(defence) { return; }

        if(myAnimator.GetBool("Idle")) { return; } 
        int action = Random.Range(0, 2);
        if(action == 0 && InRange() && CouldDamage())
        {
            Attack();
            attackTimer = 0;
        } else
        {
            Defend();
        }
        Rotate();
    }

    IEnumerator Wait(int length)
    {
        yield return new WaitForSeconds(length);
        myAnimator.SetBool("Idle", false);
    }

    public void Attack()
    {
        amtr.SetTrigger("Attack");
    }

    public void Defend()
    {
        amtr.SetBool("Defend", true);
        defence = true;
        StartCoroutine(EndDefence());
    }

    IEnumerator EndDefence()
    {
        yield return new WaitForSeconds(3);
        amtr.SetBool("Defend", false);
        defence = false;
    }

    public override bool CouldDamage()
    {
        return attackTimer > attackCD;
    }

    public override void PlayerDetected()
    {
        detected = true;
        movement.ToggleMovement(false);
    }

    public bool InRange()
    {
        if(
            Mathf.Abs(transform.position.x - player.transform.position.x) < 5
            &&
            Mathf.Abs(transform.position.y - player.transform.position.y) < 5)
        {
            return true;
        }
        return false;
    }

    public void DealDamage()
    {
        if(body.IfTouchingPlayer())
        {
            player.Hurt(Damage, "physical");

            myAnimator.SetBool("Idle", true);
            StartCoroutine(Wait(idleDuration));
        }   
    }

    public void DefendMode()
    {
        armour += DefenceStrength;
        magicalResistance += DefenceStrength;
        defence = true;
    }

    public void CancelDefendMode()
    {
        armour -= DefenceStrength;
        magicalResistance -= DefenceStrength;
        defence = false;
    }

    public override void Hurt(int damage, string type, string method)
    {
        if(defence)
        {
            Damage += 10;
            myAnimator.SetBool("Idle", true);
            StartCoroutine(Wait(idleDuration));
        }

        base.Hurt(damage, type, method);
     }

    public void Rotate()
    {
        if(myAnimator.GetBool("idle"))
        {
            return;
        } 
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x)
                , transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x)
    , transform.localScale.y);
        }
    }

    public override void PlayerNotDetected()
    {
        detected = false;
    }
}
