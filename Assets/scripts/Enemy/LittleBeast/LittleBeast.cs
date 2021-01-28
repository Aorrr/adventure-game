using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBeast : Enemy
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shootPoint;
    [SerializeField] float projectileSpeed;
    [SerializeField] float shootInterval = 2.5f;
    [SerializeField] float detectRangeX = 5f;
    [SerializeField] float detectRangeY = 3f;

    private float shootCountDown;
    private bool couldShoot = true;
    private Player player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        shootCountDown = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        shootCountDown -= Time.deltaTime;
        ShootDetect();
    }

    private void ShootDetect()
    {
        if (System.Math.Abs(player.transform.position.x - transform.position.x) < detectRangeX && 
            System.Math.Abs(player.transform.position.y - transform.position.y) < detectRangeY &&
            shootCountDown <= 0 && couldShoot)
        {
            animator.SetTrigger("breath");
            couldShoot = false;
            shootCountDown = shootInterval;
        }
    }

    public void Shoot()
    {
        GameObject fireBall = Instantiate(
                    projectile,
                    shootPoint.transform.position,
                    Quaternion.identity) as GameObject;
        fireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed * transform.localScale.x, 0);
        fireBall.GetComponent<Transform>().localScale = new Vector2(Mathf.Sign(transform.localScale.x), 1f);
        couldShoot = true;
    }

    public override void Hurt(int damage, string type, string method)
    {
        if (hp <= 0) { return; }
        float reduction = 1;
        // calculate physical damage with armour
        if (type.ToLower() == "physical")
        {
            reduction = (float)armour / ((float)armour + 100);
            reduction = 1 - reduction;
            damage = (int)Mathf.Round(reduction * damage);
        }

        // calculate maginal damage with MR
        else if (type.ToLower() == "magical")
        {
            reduction = (float)magicalResistance / ((float)magicalResistance + 100);
            reduction = 1 - reduction;
            damage = (int)Mathf.Round(reduction * damage);
        }

        popUpObject.SetDamage(damage);

        GameObject popUp = Instantiate<GameObject>
        (popUpObject.gameObject, shootPoint.transform.position, Quaternion.identity);

        Destroy(popUp, 2f);
        GameObject blood = Instantiate(hurtEffect, shootPoint.transform.position, transform.rotation);
        hp -= damage;
        Destroy(blood, 1f);
        if (hp <= 0)
            Die();
    }

}
