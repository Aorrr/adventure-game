using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    // Start is called before the first frame update
    bool inRage = false;
    Animator myAnimator;

    [SerializeField] int Damage = 1;
    void Start()
    {
        myAnimator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rage()
    {
        myAnimator.SetBool("inRage", true);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<Sanity>().LoseSanity(Damage);
        }
    }
}