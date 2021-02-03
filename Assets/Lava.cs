using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] int damage;

    Coroutine causeDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(player != null)
        {
            player.Hurt(1000, "magical");
        } 
        if (enemy != null)
        {
            enemy.Die();
        }
    }
}
