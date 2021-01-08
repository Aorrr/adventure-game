using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPotion : MonoBehaviour
{
    Light light;
    [SerializeField] float lightAmount;
    [SerializeField] GameObject pickupEffect;
    [SerializeField] float animationDuration;

    void Start()
    {
        light = FindObjectOfType<Light>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        light.changeRadius(lightAmount);
        GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(effect, animationDuration);
        Destroy(gameObject);
    }
}
