using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingScream : MonoBehaviour
{
    CircleCollider2D circle;
    Player player;
    // Start is called before the first frame update

    float sinceLastPushBack = 4f;
    float pushCD = 4f;
    Animator animator;

    void Start()
    { 

        circle = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        sinceLastPushBack += Time.deltaTime;
    }

    public void PushPlayerBack()
    {
        if (circle.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (sinceLastPushBack > pushCD)
            {
                Debug.Log("triggers push function");
                float sign = Mathf.Sign(player.transform.position.x - circle.transform.position.x);
                player.PushBack(sign *10, 0f, 1);
                sinceLastPushBack = 0;
            }
        }
    }

    public void StartScream()
    {
        animator.SetTrigger("Scream");
    }
}
