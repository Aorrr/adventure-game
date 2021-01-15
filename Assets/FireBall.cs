using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if(player.CouldHurt())
            {
                CamShakeController controller = FindObjectOfType<CamShakeController>();
                if (controller != null)
                {
                    controller.ShakeIdleAtController(0.3f, 3f, 2f);
                    controller.ShakeRunAtController(0.3f, 3f, 2f);
                }
                player.Hurt(damage, "magical");
                Destroy(gameObject);
            } 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
