using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingScream : MonoBehaviour
{
    CircleCollider2D circle;
    Player player;
    CamShakeController shaker;
    [SerializeField] AudioClip clip;

    // Start is called before the first frame update

    float sinceLastPushBack = 5f;
    float pushCD = 5f;
    Animator animator;

    void Start()
    {
        shaker = FindObjectOfType<CamShakeController>();
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
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        animator.SetTrigger("Scream");
        shaker.ShakeIdleAtController(3.5f, 1f, 1.5f);
        shaker.ShakeRunAtController(3.5f, 1.5f, 1f);

        
    }

}
