using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Animator myAnimator;
    Coroutine causeDamage;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        causeDamage = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in");
        myAnimator.SetBool("out", true);
        if(causeDamage == null)
            causeDamage = StartCoroutine(CauseDamage(collision.gameObject));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ok");
        myAnimator.SetBool("out", false);
        if(causeDamage != null)
        {
            StopCoroutine(causeDamage);
            causeDamage = null;
        }
            
    }

    IEnumerator CauseDamage(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();
        Enemy enemy = obj.GetComponent<Enemy>();
        if(player != null)
        {
            while(true)
            {
                player.Hurt(10, "physical");
                yield return new WaitForSeconds(1);
            }
        }
        else if(enemy != null)
        {
            while(true)
            {
                enemy.Hurt(10, "physical", "");
                yield return new WaitForSeconds(1);
            }
        }
    }

}
