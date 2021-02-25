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
    [SerializeField] AudioClip lightening;


    [SerializeField] Lightening ltn;
    bool charged = false;
    bool IfSummonLightening;
    int chargeGauge = 100;
    float ltnDuration = 20f;
    float timer = 0;
    int gaugeFill = 0;

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

        if (enemiesInRange.Length > 0)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.gameObject.transform.position, 0.4f);

            if(charged)
            {
                Instantiate(ltn, attackPos.position + new Vector3(0f, 6f, 0f), Quaternion.identity);
                AudioSource.PlayClipAtPoint(lightening, Camera.main.transform.position, 0.8f);
            }
        }

        for(int i = 0; i < enemiesInRange.Length; i++)
        {

            enemiesInRange[i].GetComponent<EnemyBody>().Hurt(baseDamage * DamageFactor, "physical", "sword");
            StartCoroutine(shaker.ShakeIdle(0.3f, 3f, 2f));
            StartCoroutine(shaker.ShakeRun(0.3f, 3f, 2f));

            // lightening related
            gaugeFill += baseDamage * DamageFactor;
            if(gaugeFill >= chargeGauge)
            {
                charged = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void Update()
    {
       if(charged)
        {
            timer += Time.deltaTime;
            if(timer >= ltnDuration)
            {
                charged = false;
                gaugeFill = 0;
                timer = 0;
            }
        }   
    }
}
