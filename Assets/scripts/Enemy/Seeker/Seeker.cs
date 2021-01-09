using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public int attackDamage = 20;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public AudioClip scream;
    public GameObject ripple;
    public float x_left;
    public float x_right;
    public float y_up;
    public float y_down;

    private AudioSource myAudioSource;
    private GameObject myRipple;
    private CamShakeController shaker;

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);
        foreach(Collider2D obj in hitEnemies)
        {
            obj.GetComponent<Player>().Hurt(attackDamage);
        }
    }

    public float[] GetMovementRange()
    {
        float[] range = { x_left, x_right, y_up, y_down };
        return range;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Scream()
    {
        Debug.Log("ok!");
        myAudioSource.PlayOneShot(scream);
        myRipple = Instantiate(ripple, transform.position, transform.rotation);
        shaker.ShakeIdleAtController(1.5f, 3f, 2f);
        shaker.ShakeRunAtController(1.5f, 3f, 2f);
        Destroy(myRipple, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        shaker = FindObjectOfType<CamShakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
