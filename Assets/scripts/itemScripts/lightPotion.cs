using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPotion : MonoBehaviour
{
    Light light;
    [SerializeField] float lightAmount;

    void Start()
    {
        light = FindObjectOfType<Light>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        light.changeRadius(lightAmount);
        Destroy(gameObject);
    }
}
