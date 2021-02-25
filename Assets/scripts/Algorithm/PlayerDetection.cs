using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    [SerializeField] Enemy enemy;
    BoxCollider2D box;
    CapsuleCollider2D cap;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        cap = GetComponent<CapsuleCollider2D>();
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
            enemy.PlayerDetected();
        }

        if (!cap.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            enemy.PlayerNotDetected();
        }

    }
}
