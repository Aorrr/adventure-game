using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] float attackSpeed = 2f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float arrowSpeed = 25f;
    [SerializeField] GameObject DieEffect;

    // State
    bool isAlive = true;
    bool couldHurt = true;
    float attackInterval;
    float initialSpeed;

    // cached component references
    Rigidbody2D myRidigidBody;
    Animator myAnimator;
    BoxCollider2D myFeet;
    CapsuleCollider2D myBodyCollider;
    float gravityScaleAtStart;

    // for combo system
    float reset = 0f;
    float resetTime = 1f;

    // Assistance
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject arrowPrefab;

    // Message then methods
    void Start()
    {
        myRidigidBody = GetComponent<Rigidbody2D>();

        myAnimator = GetComponentInChildren<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRidigidBody.gravityScale;
        initialSpeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        FlipSprite();
        Attack();
        BowLight();
        Slide();
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
        if(myAnimator.GetBool("isRunning")==true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                myAnimator.SetTrigger("startSlide");
            }
        } else
        {
            myAnimator.SetBool("slide", false);
        }
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


    public void Hurt(int amount)
    {
        if (couldHurt) {
            FindObjectOfType<Sanity>().LoseSanity(amount);
            myAnimator.SetTrigger("hurt");
            couldHurt = false;
            StartCoroutine(InvulnerableTime(3));
        }
    }

   IEnumerator InvulnerableTime(int invulnerableDuration)
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
            myAnimator.SetTrigger("bowLight");
        }
    }

    public void BowLightShoot()
    {
        GameObject arrow = Instantiate(
                arrowPrefab,
                shootPoint.position,
                Quaternion.identity) as GameObject;
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed * transform.localScale.x, 0);
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
            initialSpeed = 0f;
            myRidigidBody.velocity = new Vector2(0f, 0f);
            isAlive = false;
            myAnimator.SetTrigger("die");
            FindObjectOfType<Light>().SetFadeSpeed(5f);
            Time.timeScale = 0.5f;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
