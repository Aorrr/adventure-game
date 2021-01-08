using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    [SerializeField] Enemy enemy;
    BoxCollider2D box;
    CircleCollider2D circle;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if(box.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            enemy.ToggleRage(true);
        } else if (!circle.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            enemy.ToggleRage(false);
        }
    }
}
