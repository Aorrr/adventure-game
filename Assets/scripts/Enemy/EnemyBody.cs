using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int hurtFactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
