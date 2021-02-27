using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Player player;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Run", player.GetRunState());
        animator.SetBool("Idle", player.GetIdleState());
    }

    public void EnterCutScene()
    {
        animator.SetBool("CutScene", true);
        player.EnterCutScene(true);
    }

    public void OutOfCutScene()
    {
        animator.SetBool("CutScene", false);
        player.EnterCutScene(false);
    }
}
