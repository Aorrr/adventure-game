using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] int damage = 100;

    private bool isHitted = false;
    private float timeToDestroy = 0;

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (isHitted)
                return;
            CamShakeController controller = FindObjectOfType<CamShakeController>();
            if (controller != null)
            {
                controller.ShakeIdleAtController(0.3f, 3f, 2f);
                controller.ShakeRunAtController(0.3f, 3f, 2f);
            }
            player.Hurt(damage);
            isHitted = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
