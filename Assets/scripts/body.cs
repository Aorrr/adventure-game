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

    public void stop(float amount)
    {
        player.slowDown(amount);
    }

    public void release(float amount)
    {
        player.speedUp(amount);
    }
}
