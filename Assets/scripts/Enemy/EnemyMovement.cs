using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    BoxCollider2D horiDetector;
    CapsuleCollider2D vertDetector;
    Rigidbody2D myRigidBody;
    Transform parentTransform;
    [SerializeField] Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = enemy.transform;
        myRigidBody = GetComponentInParent<Rigidbody2D>();
        horiDetector = GetComponent<BoxCollider2D>();
        vertDetector = GetComponent<CapsuleCollider2D>();
    }

    private bool isFacingLeft()
    {
        return parentTransform.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemy.IfRage())
        {
            checkCollision();
            if (isFacingLeft()) { myRigidBody.velocity = new Vector2(-moveSpeed, 0f); }
            else
            {
                myRigidBody.velocity = new Vector2(moveSpeed, 0f);
            }
        } else
        {
            myRigidBody.velocity = new Vector2(0f, 0f);
        }
    }

    private void checkCollision()
    {
        if(!vertDetector.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            horiDetector.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            parentTransform.localScale = new Vector2((Mathf.Sign(myRigidBody.velocity.x)), 1f);
        }
    }

    public void changeMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
