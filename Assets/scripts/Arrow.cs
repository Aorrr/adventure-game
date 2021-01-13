using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] int damage = 100;

    private bool isHitted = false;
    private float timeToDestroy = 0;

    Coroutine shakeIdle;
    Coroutine shakeRun;

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
            enemy.Hurt(damage, "physical");
            isHitted = true;
            GetComponent<SpriteRenderer>().enabled = false;
        } 
        else
        {
            if(shakeIdle != null && shakeRun != null)
            {
                StopCoroutine(shakeIdle);
                StopCoroutine(shakeRun);
            }
            Destroy(gameObject);
        }
    }
}
