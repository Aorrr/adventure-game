using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightening : MonoBehaviour
{
    BoxCollider2D box;
    bool couldDmg = false;
    int dmg = 100;

    public void SetDmg()
    {
        couldDmg =!couldDmg;
    }
    // destroyed at the end of the frame
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if(!couldDmg) { return; }

        EnemyBody enemy = collision.GetComponent<EnemyBody>();
        if (enemy != null)
        {
            enemy.Hurt(dmg, "magical", "melee");
        }
    }
}
