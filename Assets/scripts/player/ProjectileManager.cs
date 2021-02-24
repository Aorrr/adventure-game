using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] Image[] arrows;
    [SerializeField] float refillTime = 1f;
    [SerializeField] int maxProjectileAmt = 5;
    [SerializeField] float timer = 0;
    [SerializeField] float refillFactor = 1f;

    int availProj = 0;

    Color inactive;
    Color active;

    // Start is called before the first frame update
    void Start()
    {
        inactive = new Color(126 / 255f, 126f / 255, 126f / 255);
        active = Color.white;
        availProj = 0;
    }

    // Update is called once per frame
    void Update()
    {

       foreach(Image img in arrows)
        {
            img.color = inactive;
        }

       if(availProj < maxProjectileAmt)
        {
            timer += Time.deltaTime * refillFactor;

            if(timer >= refillTime)
            {
                availProj += 1;
                timer = 0;
            }
        }

        UpdateAvailArrows();
    }

    public void UpdateAvailArrows()
    {
        for(int i = 0; i < maxProjectileAmt; i++)
        {
            if(i < availProj)
            {
                arrows[i].color = active;
            } else
            {
                arrows[i].color = inactive;
            }
        }
    }

    public bool UseArrow(int amount)
    {
        if(availProj >= amount)
        {
            availProj -= amount;
            return true;
        } else
        {
            return false;
        }
    }

    public void AllRed()
    {
        foreach(Image img in arrows)
        {
            img.color = Color.red;
        }
    }
}
