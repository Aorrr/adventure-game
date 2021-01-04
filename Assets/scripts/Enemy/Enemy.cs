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
    private float timeSinceAttack;
    [SerializeField] float attackInterval;
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
        Debug.Log(timeSinceAttack);
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
}