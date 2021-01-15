using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBeast : Enemy
{
    [SerializeField] GameObject projectile1;
    [SerializeField] GameObject projectile2;
    [SerializeField] GameObject shootPoint;
    [SerializeField] float projectileSpeed;
    [SerializeField] float shootInterval = 2.5f;
    [SerializeField] AudioClip scream;
    [SerializeField] CamShakeController camShakeController;
    [SerializeField] GameObject ripple;

    private float shootCountDown;

    public void Start()
    {
        maxHealth = hp;
        ccollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        shootCountDown = shootInterval;
    }

    public void Update()
    {
        shootCountDown -= Time.deltaTime;
        ActivateBurnt();
        LookAtPlayer();
    }

    public float GetShootCountDown()
    {
        return shootCountDown;
    }

    public void ActivateBurnt()
    {
        if(GetHealthPercentage() <= 0.5)
        {
            myAnimator.SetTrigger("burnt");
        }
    }

    public void Burnt()
    {
        camShakeController.ShakeIdleAtController(2f, 4f, 3f);
        camShakeController.ShakeRunAtController(2f, 4f, 3f);
    }

    public void Shoot1()
    {
        GameObject fireBall = Instantiate(
                projectile1,
                shootPoint.transform.position,
                Quaternion.identity) as GameObject;
        fireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed * transform.localScale.x, 0);
        fireBall.GetComponent<Transform>().localScale = new Vector2(Mathf.Sign(transform.localScale.x), 1f);
        shootCountDown = shootInterval;
    }

    public void Shoot2()
    {
        
        GameObject fireBall1 = Instantiate(
                projectile2,
                shootPoint.transform.position,
                Quaternion.identity) as GameObject;
        GameObject fireBall2 = Instantiate(
                projectile2,
                shootPoint.transform.position,
                Quaternion.identity) as GameObject;
        fireBall1.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed * transform.localScale.x, 0);
        fireBall1.GetComponent<Transform>().localScale = new Vector2(Mathf.Sign(transform.localScale.x), 1f);
        fireBall1.GetComponent<Transform>().Rotate(new Vector3(0, 0, 0));
        fireBall2.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed * transform.localScale.x, projectileSpeed * 0.34f);
        fireBall2.GetComponent<Transform>().localScale = new Vector2(Mathf.Sign(transform.localScale.x), 1f);
        fireBall2.GetComponent<Transform>().Rotate(new Vector3(0, 0, -20 * Mathf.Sign(transform.localScale.x)));
        shootCountDown = shootInterval;
    }
    
    public void Scream()
    {
        AudioSource.PlayClipAtPoint(scream, transform.position);
        camShakeController.ShakeIdleAtController(1.3f, 2f, 2f);
        camShakeController.ShakeRunAtController(1.3f, 2f, 2f);
        GameObject myRipple = Instantiate(ripple, shootPoint.transform.position, shootPoint.transform.rotation);
        Destroy(myRipple, 1f);
        FindObjectOfType<Player>().PushBack(-7.5f * transform.localScale.x , 10f, 1);
    }

    public void LookAtPlayer()
    {
        if (FindObjectOfType<Player>().transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        } else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    public override void Hurt(int damage, string type)
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
