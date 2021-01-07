using System.Collections;
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
    [SerializeField] GameObject hurtEffect;
    [SerializeField] int armour;
    [SerializeField] int magicalResistance;

    private float timeSinceAttack;
    private bool canDamage = true;
    private int maxHealth;
    void Start()
    {
        maxHealth = hp;
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

    public void Hurt(int damage, string type)
    {
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
        GameObject blood = Instantiate(hurtEffect, transform.position, transform.rotation);
        hp -= damage;
        Destroy(blood, 1f);
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        //play death animation
        //play particle effect
    }

    public float GetHealthPercentage()
    {
        return (float)hp / (float)maxHealth;
    }
}