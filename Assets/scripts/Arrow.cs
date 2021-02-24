using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Arrow : MonoBehaviour
{
    [SerializeField] int damage = 10;

    private bool isHitted = false;
    private float timeToDestroy = 0;

    Coroutine shakeIdle;
    Coroutine shakeRun;

    StatsManager stats;

    private void Start()
    {
        stats = FindObjectOfType<StatsManager>();
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBody enemy = collision.GetComponent<EnemyBody>();
        if(enemy != null)
        {
            if (isHitted)
                return;
            CamShakeController controller = FindObjectOfType<CamShakeController>();
            if(controller != null)
            {
                shakeIdle = StartCoroutine(controller.ShakeIdle(0.3f, 3f, 2f));
                shakeRun = StartCoroutine(controller.ShakeRun(0.3f, 3f, 2f));
            }
            enemy.Hurt(damage, "magical", "arrow");

            /*
            if(stats.IfExecutionUnlocked() && enemy.GetComponentInParent<Enemy>()
                .GetHealthPercentage()*100 <= stats.GetExecutionThreshold())
            {
                enemy.GetComponentInParent<Enemy>().Execute();
                shakeIdle = StartCoroutine(controller.ShakeIdle(0.2f, 6f, 3f));
                shakeRun = StartCoroutine(controller.ShakeRun(0.2f, 6f, 3f));
            }*/

            isHitted = true;
            GetComponent<SpriteRenderer>().enabled = false;

            DestroyThis();
        } 
        else
        {
            if(shakeIdle != null && shakeRun != null)
            {
                StopCoroutine(shakeIdle);
                StopCoroutine(shakeRun);
            }
        }
    }

    public void IncreaseDamage(int amount)
    {
        if(amount < 0) { return; }
        damage += amount;
    }

    public void SetInitialDamage(int initialDmg)
    {
        damage = initialDmg;
    }

    public void DestroyThis()
    {
        Light2D light = GetComponent<Light2D>();

        if (light)
        {
            light.enabled = false;
        } 
        Destroy(gameObject);
    }

    public void IncreaseLightIntensity(int amount)
    {
        Light2D light = GetComponent<Light2D>();

        if (!light) { return;}

        light.intensity += amount;
        light.pointLightOuterRadius += 1;
    }
}
