using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy: MonoBehaviour
{
    // Start is called before the first frame update
    bool inRage = false;
    Animator myAnimator;
    BoxCollider2D ccollider;


    [SerializeField] int Damage = 1;
    [SerializeField] int hp = 30;
    [SerializeField] GameObject hurtEffect;
    [SerializeField] int armour;
    [SerializeField] int magicalResistance;
    [SerializeField] DamagePopUp popUpObject;
    [SerializeField] int exp;

    private bool canDamage = true;
    private int maxHealth;
   
    void Start()
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
        return myAnimator.GetBool("inRage");
    }

    public void Hurt(int damage, string type)
    {
        if (hp <= 0) { return; }
        float reduction = 1;
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
        hp -= damage;
        Destroy(blood, 1f);
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        FindObjectOfType<Exp>().GainExp(exp);
        Destroy(gameObject);
    }

    public float GetHealthPercentage()
    {
        return (float)hp / (float)maxHealth;
    }

    public float GetHealth()
    {
        return hp;
    }
}