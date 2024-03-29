﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [Header("Player Config")]
    [SerializeField] float slideCD = 1f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] GameObject DieEffect;
    [SerializeField] float invulnerableTime = 3f;

    // State
    bool isAlive = true;
    bool couldHurt = true;
    float attackInterval;
    float initialSpeed;
    bool ifIdle = false;
    List<string> bag;

    // cached component references
    Rigidbody2D myRidigidBody;
    Animator myAnimator;
    BoxCollider2D myFeet;
    CapsuleCollider2D myBodyCollider;
    float gravityScaleAtStart;

    // for combo system
    float reset = 0f;
    float resetTime = 1f;

    // for slide timer
    float sinceLastSlide = 5f;


    [Header("Arrow Setting")]
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform shootPoint;
    [SerializeField] Animator hurtEffectAnimator;
    [SerializeField] AudioClip shootSFX;

    float arrowSpeed = 25f;
    GameObject arrowPrefab;
    StatsManager stats;
    ProjectileManager projManager;

    // for colour change
    [SerializeField] SpriteRenderer bodyRenderer;
    bool sliding = false;


    // Message then methods
    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        myRidigidBody = GetComponent<Rigidbody2D>();
        arrowPrefab = stats.GetArrowPrefab();
        bag = new List<string>();

        myAnimator = GetComponentInChildren<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRidigidBody.gravityScale;
        initialSpeed = runSpeed;
        projManager = FindObjectOfType<ProjectileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive || myAnimator.GetBool("CutScene")) { return; }
        if(myAnimator.GetBool("controllable"))
        {
            Run();
            Jump();
            FlipSprite();
            Attack();
            BowLight();
            Slide();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRidigidBody.velocity.y);
        myRidigidBody.velocity = playerVelocity;
    }

    private void Slide()
    {
        sinceLastSlide += Time.deltaTime;

        if(myAnimator.GetBool("isRunning")==true)
        {
            if (Input.GetKeyDown(KeyCode.S) && sinceLastSlide > slideCD)
            {
                myAnimator.SetTrigger("startSlide");
                sinceLastSlide = 0;
            }
        } 
        else
        {
            myAnimator.SetBool("slide", false);
        }
    }

    public void SetSlidingStatus(bool state)
    {
        sliding = state;
    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            myAnimator.SetBool("isJumping", true);
            return;
        } else
        {
            myAnimator.SetBool("isJumping", false);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            myAnimator.SetBool("slide", false);
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRidigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRidigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            if(myAnimator.GetBool("isJumping") == false) { myAnimator.SetBool("isRunning", true); }
            transform.localScale = new Vector2(Mathf.Sign(myRidigidBody.velocity.x)*-1, 1f);
        } else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    public void Hurt(int damage, string type)
    {
        if(sliding) { return; }

        if(type.ToLower() == "magical")
        {
            damage =stats.TakeMagicalDamage(damage);
        } else if(type.ToLower() == "physical")
        {
            damage = stats.TakePhysicalDamage(damage);
        } else if(type.ToLower() != "true")
        {
            damage = 0;
            Debug.Log("wrong damage type provided");
        }

        FindObjectOfType<Sanity>().LoseSanity(damage);
        myAnimator.SetTrigger("hurt");
        StartCoroutine(InvulnerableTime(invulnerableTime));
        hurtEffectAnimator.SetTrigger("hurt");
    }

    public bool CouldHurt()
    {
        return !sliding;
    }

   IEnumerator InvulnerableTime(float invulnerableDuration)
    {

        yield return new WaitForSeconds(invulnerableDuration);
        couldHurt = true;
    }

    public void Attack()
    {
        if(reset < resetTime)
        {
            reset += Time.deltaTime;
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                myAnimator.SetTrigger("attack");
                reset = 0;
            }
            else if (CrossPlatformInputManager.GetButtonDown("Fire2"))
            {
                myAnimator.SetTrigger("attackHeavy");
                reset = 0;
            }
        }
        else
        {
            myAnimator.SetTrigger("reset");
            reset = 0;
        }
    }

    private void BowLight()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire3"))
        {
            if (projManager.UseArrow(2))
            {
                myAnimator.SetTrigger("bowLight");
            } else
            {
                projManager.AllRed();
            }
        }
    }

    public void BowLightShoot()
    {
        if(shootSFX)
        {
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);
        }
            GameObject arrow = Instantiate(
        arrowPrefab,
        shootPoint.position,
        Quaternion.identity) as GameObject;
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-stats.GetArrowSpeed() * transform.localScale.x, 0);
            arrow.GetComponent<Transform>().localScale = new Vector2(-Mathf.Sign(transform.localScale.x), 1f);

    }


    public void SetSpeed(float speed)
    {
        if(speed > 0)
        {
            runSpeed = speed;
        }
    }

    public float GetSpeed()
    {
        return runSpeed;
    }

    public void SetDefaultSpeed()
    {
        runSpeed = initialSpeed;
    }

    public void Die()
    {
        if(isAlive)
        {
            Vector3 pos = transform.position;
            pos.y -= 0.7f;
            GameObject light = Instantiate(DieEffect, pos, transform.rotation);
            myRidigidBody.velocity = new Vector2(0f, 0f);

            isAlive = false;
            myAnimator.SetTrigger("die");
            FindObjectOfType<Light>().SetFadeSpeed(3f);
            //Time.timeScale = 0.5f;
            GameObject.Find("SceneFade").GetComponent<Animator>().SetTrigger("fade");
            StartCoroutine(ShowGameOverCanvas());
        }
    }

    IEnumerator ShowGameOverCanvas()
    {
        yield return new WaitForSeconds(3);
        GameObject.Find("UI").transform.GetChild(2).gameObject.SetActive(true);
    }

    public void SlowDown(float slowFactor, int duration)
    {
        if(slowFactor < 1 && slowFactor > 0)
        {
            StartCoroutine(SlowMovement(duration, initialSpeed * slowFactor));
        }
    }

    IEnumerator SlowMovement(int duration, float newSpeed)
    {
        float speed = initialSpeed;
        float initialJumpSpeed = jumpSpeed;
        initialSpeed = newSpeed;
        jumpSpeed = jumpSpeed * newSpeed/speed;

        bodyRenderer.material.color = new Color(124/255f, 199/255f, 255/255f);
        yield return new WaitForSeconds(duration);

        bodyRenderer.material.color = Color.white;
        initialSpeed = speed;
        jumpSpeed = initialJumpSpeed;
    }

    public void PushBack(float xVelocity, float yVelocity, int duration)
    {
        myRidigidBody.velocity = new Vector2(xVelocity,  yVelocity);
        StartCoroutine(Uncontrollable(duration));
    }

    IEnumerator Uncontrollable(int duration)
    {
        Debug.Log("not controllable");
        myAnimator.SetBool("controllable", false);
        float speed = initialSpeed;

        yield return new WaitForSeconds(duration);
        myAnimator.SetBool("controllable", true);
    }

    public void ReceiveItem(string itemName)
    {
        bag.Add(itemName);
        foreach(string item in bag)
        {
            Debug.Log(item);
        }
        
    }

    public bool HasItem(string itemName)
    {
        foreach(string item in bag)
        {
            if(item.Equals(itemName))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetRunState()
    {
        return myAnimator.GetBool("isRunning");
    }

    public void ToggleIdleState(bool state)
    {
        ifIdle = state;
    }

    public bool GetIdleState()
    {
        return ifIdle;
    }

    public void EnterCutScene(bool state)
    {
        myAnimator.SetBool("CutScene", state);
    }

}
