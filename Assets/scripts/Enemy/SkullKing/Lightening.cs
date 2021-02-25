using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightening : MonoBehaviour
{
    BoxCollider2D box;
    bool couldDmg = false;
    int dmg = 100;
    // Start is called before the first frame update


    public void SetDmg()
    {
        couldDmg = !couldDmg;
    }
    // destroyed at the end of the frame
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBody enemy = collision.gameObject.GetComponent<EnemyBody>();
        if (enemy != null)
        {
            Debug.Log("collided");
            enemy.Hurt(dmg, "magical", "melee");
        }
    }
}
