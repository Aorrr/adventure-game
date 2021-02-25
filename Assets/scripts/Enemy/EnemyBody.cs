using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int hurtFactor = 1;
    CircleCollider2D circle;
    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int amount, string type, string method)
    {
        enemy.Hurt(amount * hurtFactor, type, method);
        enemy.GetComponent<Animator>().SetTrigger("TakeDamage");
    }

    public bool IfTouchingPlayer()
    {
        if (circle.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            return true;
        } else
        {
            return false;
        }
    }
}
