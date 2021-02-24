using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int damage = 10;
    [SerializeField] int frostDuration = 3;
    [SerializeField] float slowFactor = 0.5f;

    float lightDuration = 0.5f;
    float timer = 0;
    bool count = false;

    Light2D blueLight;

    Animator animator;
    BoxCollider2D box;
    CircleCollider2D circle;
    Player player;
    SkullKing king;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        blueLight = GetComponent<Light2D>();
        blueLight.enabled = false;
        king = GetComponentInParent<SkullKing>();
    }

    public void CheckPlayerCollision()
    {
        if (box.IsTouchingLayers(LayerMask.GetMask("Player")) ||
            circle.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if(!player.CouldHurt()) { return; }
            FindObjectOfType<Player>().Hurt(damage + king.GetDmg(), "magical");
            FindObjectOfType<Player>().SlowDown(slowFactor, frostDuration);
        }
    }

    public void Update()
    {
        if(count)
        {
            timer += Time.deltaTime;
            if(timer > lightDuration)
            {
                count = false;
                blueLight.enabled = false;
            }
        } 
    }

    public void StartFire()
    {

        animator.SetTrigger("Fire");

        blueLight.enabled = true;
        count = true;
    }
}
