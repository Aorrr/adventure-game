using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hittedObj = collision.gameObject;
        hittedObj.GetComponent<Enemy>().Hurt(damage);
    }
}
