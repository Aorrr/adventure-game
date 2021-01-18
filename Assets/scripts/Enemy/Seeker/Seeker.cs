using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Enemy
{
    public int attackDamage = 20;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public float x_left;
    public float x_right;
    public float y_up;
    public float y_down;

    private AudioSource myAudioSource;
    private GameObject myRipple;
    private CamShakeController shaker;

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);
        foreach(Collider2D obj in hitEnemies)
        {
            obj.GetComponent<Player>().Hurt(attackDamage, "physical");
        }
    }

    public override void Hurt(int damage, string type, string method)
    {
        if (hp <= 0) { return; }
        float reduction = 1;
        // calculate physical damage with armour
        if (type.ToLower() == "physical")
        {

            reduction = (float)armour / ((float)armour + 100);
            reduction = 1 - reduction;
            damage = (int)Mathf.Round(reduction * damage);
        }

        // calculate maginal damage with MR
        else if (type.ToLower() == "magical")
        {
            reduction = (float)magicalResistance / ((float)magicalResistance + 100);
            reduction = 1 - reduction;
            damage = (int)Mathf.Round(reduction * damage);
        }

        popUpObject.SetDamage(damage);

        // pop up effect
        GameObject popUp = Instantiate<GameObject>
        (popUpObject.gameObject, transform.position, Quaternion.identity);
        Destroy(popUp, 2f);

        // blood effect
        GameObject blood = Instantiate(hurtEffect, transform.position, transform.rotation);
        hp -= damage;
        Destroy(blood, 1f);

        // trigger hurt animation
        myAnimator.SetTrigger("hurt");

        if (hp <= 0)
            Die();
    }

    public float[] GetMovementRange()
    {
        float[] range = { x_left, x_right, y_up, y_down };
        return range;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        shaker = FindObjectOfType<CamShakeController>();
        maxHealth = hp;
        ccollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
