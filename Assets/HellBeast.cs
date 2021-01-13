using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBeast : Enemy
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shootPoint;
    [SerializeField] float projectileSpeed;

    public void Shoot()
    {
        GameObject fireBall = Instantiate(
                projectile,
                shootPoint.transform.position,
                Quaternion.identity) as GameObject;
        fireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed * transform.localScale.x, 0);
        fireBall.GetComponent<Transform>().localScale = new Vector2(Mathf.Sign(transform.localScale.x), 1f);
    }
}
