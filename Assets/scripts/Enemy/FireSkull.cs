using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkull : MonoBehaviour
{

    Enemy enemy;
    float timeAfterLastAttack;
    float timeInterval;
    Player player;
    bool couldDamage = false;
    EnemyBody body;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();
        timeAfterLastAttack = 0;
        timeInterval = 1f;
        body = GetComponentInChildren<EnemyBody>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!couldDamage) { timeAfterLastAttack += Time.deltaTime; }
        if (enemy.IfRage())
        {
            if(timeAfterLastAttack >= timeInterval)
            {
                StartCoroutine(FireSkullLeapAttack());
                timeAfterLastAttack = 0;
            }
        }
        myAnimator.SetBool("Fire", couldDamage);
        DamagePlayer();
    }

    IEnumerator FireSkullLeapAttack()
    {
        couldDamage = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Vector2 direction = player.transform.position - enemy.transform.position;
        direction.y = 0;
        direction.x = Mathf.Sign(direction.x) * 15;

        transform.localScale = new Vector2(-Mathf.Sign(direction.x) *
Mathf.Abs(transform.localScale.x), transform.localScale.y);

        yield return new WaitForSeconds(2);
       
        GetComponent<Rigidbody2D>().velocity += direction;

    }

    public void DamagePlayer()
    {
        if(!couldDamage) { return; }
        if (body.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Sanity>().LoseSanity(enemy.GetDamage());
            couldDamage = false;
        }
    }

    public void ToggleAttackStatus(bool status)
    {
        couldDamage = status;
    }
 }
