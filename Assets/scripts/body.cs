using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bowLightShoot()
    {
        player.BowLightShoot();
    }

    public void SpeedUp(float amount)
    {
        if(amount > 1)
        {
            player.SetSpeed(player.GetSpeed() * amount);
        }
    }

    public void SlowDown(float amount)
    {
        if(amount > 0 && amount < 1)
        {
            player.SetSpeed(player.GetSpeed() * amount);
        }
    }

    public void ResetSpeed()
    {
        player.SetDefaultSpeed();
    }

    public void AddVertiSpeed(float amount)
    {
        if (amount > 0)
        {
            player.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, amount);
        }
        else
        {
            Debug.Log("Please enter a positive amount for AddVertiSpeed");
        }
           
    }

    public void Attack(int damageFactor)
    {
        FindObjectOfType<MeleeAttack>().Attack(damageFactor);
    }
}
