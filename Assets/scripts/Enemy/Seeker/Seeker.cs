using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public int attackDamage = 20;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public float x_left;
    public float x_right;
    public float y_up;
    public float y_down;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
