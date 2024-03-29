﻿using System;
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
    bool isStatic = false;

    float unStuckTime = 0;
    float minUnStuckTime = 1f;
    float leapAttempt = 1;
    bool couldMove = true;

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
        if(!couldMove) {
            myRigidBody.velocity = new Vector2(0f, 0f);
            return; }
        else
        {
            changeMoveSpeed(1);
        }
        if(isStatic) {
            return; }
        if(!enemy.CouldDamage())
        {
            checkCollision();
            if (isFacingLeft()) { myRigidBody.velocity = new Vector2(-moveSpeed, 0f); }
            else
            {
                myRigidBody.velocity = new Vector2(moveSpeed, 0f);
            }
        }
    }

    private void checkCollision()
    {
        if(!vertDetector.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            horiDetector.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            parentTransform.localScale = new Vector2((Mathf.Sign(myRigidBody.velocity.x))*
                Mathf.Abs(parentTransform.localScale.x), parentTransform.localScale.y);
        }

        if(!vertDetector.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
            horiDetector.IsTouchingLayers(LayerMask.GetMask("Ground"))) {

            myRigidBody.velocity += new Vector2(0f, -4f);
        }
    }

    public void changeMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void StopForSeconds(int interval) {
        StartCoroutine(Stop(interval));
    }

    IEnumerator Stop(int interval)
    {
        isStatic = true;
        yield return new WaitForSeconds(interval);
        isStatic = false;
    }

    public void ToggleMovement(bool status)
    {
        couldMove = status;
    }

}
