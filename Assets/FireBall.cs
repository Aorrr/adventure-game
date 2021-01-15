using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] GameObject explosion;
    CamShakeController controller;

    private void Start()
    {
        controller = FindObjectOfType<CamShakeController>();
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if(player.CouldHurt())
            {

                if (controller != null)
                {
                    controller.ShakeIdleAtController(0.3f, 3f, 2f);
                    controller.ShakeRunAtController(0.3f, 3f, 2f);
                }
                GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(exp, 1.8f);
                player.Hurt(damage, "magical");
                Destroy(gameObject);
            }
        }
        else
        {
            controller.ShakeIdleAtController(0.3f, 3f, 2f);
            controller.ShakeRunAtController(0.3f, 3f, 2f);
            GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(exp, 1.8f);
            Destroy(gameObject);
        }
    }
}
