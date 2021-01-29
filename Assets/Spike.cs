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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in");
        myAnimator.SetBool("out", true);
        causeDamage = StartCoroutine(CauseDamage(collision.gameObject));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ok");
        myAnimator.SetBool("out", false);
        StopCoroutine(causeDamage);
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
