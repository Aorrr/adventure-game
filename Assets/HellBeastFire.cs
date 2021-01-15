using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBeastFire : MonoBehaviour
{
    public int damage = 50;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null)
        {
            Debug.Log("okkkk!!!");
            player.PushBack(5f, 5f, 0);
            player.Hurt(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.PushBack(5f, 5f, 0);
            player.Hurt(damage);
        }
    }
}
