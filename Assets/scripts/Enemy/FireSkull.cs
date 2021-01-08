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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();
        timeAfterLastAttack = 0;
        timeInterval = 1.5f;
        body = GetComponentInChildren<EnemyBody>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterLastAttack += Time.deltaTime;
        if (enemy.IfRage())
        {
            if(timeAfterLastAttack >= timeInterval)
            {
                StartCoroutine(FireSkullLeapAttack());
                timeAfterLastAttack = 0;
            }
        }
        DamagePlayer();
    }

    IEnumerator FireSkullLeapAttack()
    {
        couldDamage = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        Vector2 direction = player.transform.position - enemy.transform.position;
        direction.y = 0;
        direction.x = Mathf.Sign(direction.x) * 15;
        GetComponent<Rigidbody2D>().velocity += direction;
    }

    public void DamagePlayer()
    {
        if(!couldDamage) { return; }
        if (body.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Sanity>().LoseSanity(enemy.GetDamage());
            Debug.Log("hurt: " + enemy.GetDamage());
            couldDamage = false;
        }
    }
}
