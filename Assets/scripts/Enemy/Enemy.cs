﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy: MonoBehaviour
{
    // Start is called before the first frame update   
    protected Animator myAnimator;
    protected BoxCollider2D ccollider;


    [SerializeField] protected int Damage = 1;
    [SerializeField] protected int hp = 30;
    [SerializeField] protected GameObject hurtEffect;
    [SerializeField] protected int armour;
    [SerializeField] protected int magicalResistance;
    [SerializeField] protected DamagePopUp popUpObject;
    [SerializeField] protected int exp;

    //Drop item settings
    [SerializeField] protected GameObject dropItem;
    [SerializeField] protected GameObject dropPosition;


    //[SerializeField] GameObject ExecutionEffect;

    protected bool inRage = false;
    protected bool canDamage = true;
    protected int maxHealth;
   
    void Start()
    {
        maxHealth = hp;
        ccollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    public void PrepareStats()
    {
        maxHealth = hp;
        ccollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }


    public void ToggleRage(bool status)
    {
        myAnimator.SetBool("inRage", status);
    }

    public bool IfRage()
    {
        if (myAnimator == null)
            Debug.Log("oh no!!");
        return myAnimator.GetBool("inRage");
    }

    public virtual void PlayerDetected()
    {
        return;
    }

    public virtual void PlayerNotDetected()
    {
        return;
    }

    public virtual void Hurt(int damage, string type, string method)
    {
        if (hp <= 0) { return; }
        float reduction = 0;
        // calculate physical damage with armour
        if(type.ToLower() == "physical")
        {
            
            reduction = (float)armour / ((float)armour + 100);
            reduction = 1 - reduction;
            damage = (int)Mathf.Round(reduction * damage);
        }

        // calculate maginal damage with MR
        else if(type.ToLower() == "magical")
        {
            reduction = (float)magicalResistance / ((float)magicalResistance + 100);
            reduction = 1 - reduction;
            damage = (int)Mathf.Round(reduction * damage);
        }

        popUpObject.SetDamage(damage);

        GameObject popUp = Instantiate<GameObject>
        (popUpObject.gameObject, transform.position, Quaternion.identity);

        Destroy(popUp, 2f);

        GameObject blood = Instantiate(hurtEffect, transform.position, transform.rotation);
        Destroy(blood, 1f);
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        FindObjectOfType<Exp>().GainExp(exp);
        if(dropItem != null && dropPosition != null)
            Instantiate(dropItem, dropPosition.transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public float GetHealthPercentage()
    {
        if(maxHealth == 0) { return 0; }
        return (float)hp / (float)maxHealth;
    }

    public float GetHealth()
    {
        return hp;
    }

    public int GetDamage()
    {
        return Damage;
    }

    public virtual bool CouldDamage()
    {
        return myAnimator.GetBool("Fire");
    }

    public void Execute()
    {
        Destroy(gameObject);
        //GameObject execution = Instantiate(ExecutionEffect, transform.position, transform.rotation);
        //Destroy(execution, 1f);
    }
}