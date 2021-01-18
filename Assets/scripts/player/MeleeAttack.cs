using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// source: https://www.youtube.com/watch?v=1QfxdUpVh5I&t=270s

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] Transform attackPos;
     LayerMask enemyLayer;
    [SerializeField] float attackRange;
    [SerializeField] int baseDamage;
    [SerializeField] CamShakeController shaker;
    [SerializeField] AudioClip clip;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    public int GetBaseDamage()
    {
        return baseDamage;
    }

    public void Attack(int DamageFactor)
    {
        if(DamageFactor < 0)
        {
            Debug.Log("Damage factor for Attack() cannot be smaller than 0");
            return;
        }
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPos.position
            , attackRange, enemyLayer);

        if(enemiesInRange.Length > 0)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.gameObject.transform.position);
        }

        for(int i = 0; i < enemiesInRange.Length; i++)
        {
            enemiesInRange[i].GetComponent<EnemyBody>().Hurt(baseDamage * DamageFactor, "physical", "sword");
            Debug.Log(enemiesInRange[i].name);
            StartCoroutine(shaker.ShakeIdle(0.3f, 3f, 2f));
            StartCoroutine(shaker.ShakeRun(0.3f, 3f, 2f));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
