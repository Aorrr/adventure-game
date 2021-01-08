using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sanityPotion : MonoBehaviour
{
    [SerializeField] float healAmount;
    [SerializeField] GameObject pickupEffect;
    [SerializeField] float animationDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<Sanity>().Recover(healAmount);
        Debug.Log("picked up a potion!");
        GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(effect, animationDuration);
        Destroy(gameObject);
    }
}
