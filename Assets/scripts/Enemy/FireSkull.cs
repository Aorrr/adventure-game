using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkull : Enemy
{
    float timeAfterLastAttack;
    float timeInterval;
    Player player;
    bool couldDamage = false;
    EnemyBody body;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = hp;
        ccollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        timeAfterLastAttack = timeInterval;
        timeInterval = 2f;
        body = GetComponentInChildren<EnemyBody>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterLastAttack += Time.deltaTime;
        if (IfRage())
        {
            if (timeAfterLastAttack >= timeInterval)
            {
                StartCoroutine(FireSkullLeapAttack());
            }
        }
        DamagePlayer();
    }

    IEnumerator FireSkullLeapAttack()
    {
        timeAfterLastAttack = 0;
        myAnimator.SetBool("Fire", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Vector2 direction = player.transform.position - transform.position;
        direction.y = 0;
        direction.x = Mathf.Sign(direction.x) * 20;

        transform.localScale = new Vector2(-Mathf.Sign(direction.x) *
Mathf.Abs(transform.localScale.x), transform.localScale.y);

        yield return new WaitForSeconds(2);
        couldDamage = true;
        GetComponent<Rigidbody2D>().velocity = direction;
        StartCoroutine(StopRage());
    }

    IEnumerator StopRage()
    {
        yield return new WaitForSeconds(1);
        if(couldDamage)
        {
            myAnimator.SetBool("Fire", false);
            timeAfterLastAttack = 0;
            couldDamage = false;
        }

    }

    public void DamagePlayer()
    {
        if(!couldDamage) { return; }
        if (body.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            player.Hurt(GetDamage(), "physical");
            couldDamage = false;
            myAnimator.SetBool("Fire", false);
            timeAfterLastAttack = 0;
        }
    }

    public void ToggleAttackStatus(bool status)
    {
        couldDamage = status;
    }

    public override void PlayerDetected()
    {
        ToggleRage(true);
    }

    public override void PlayerNotDetected()
    {
        ToggleRage(false);
    }

    public void BeStatic(int interval)
    {
        if(myAnimator != null)
        {
            myAnimator.SetTrigger("idle");
        } else
        {
            Start();
            myAnimator.SetTrigger("idle");
        }
        GetComponentInChildren<EnemyMovement>().StopForSeconds(interval);
    }
 }
