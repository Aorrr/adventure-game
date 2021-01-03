using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    [SerializeField] float sanity;
    [SerializeField] float sanityLossSpeed;
    float initialSanity;

    public void SetSanityLossSpeed(float amount)
    {
        sanityLossSpeed = amount;
    }

    public float GetInitialSanity()
    {
        return initialSanity;
    }

    public float GetSanity()
    {
        return sanity;
    }

    public float GetSanityLossSpeed()
    {
        return sanityLossSpeed;
    }

    public void LoseSanity(float amount)
    {
        if(sanity > 0)
        {
            sanity = Mathf.Max(0, sanity - amount);
        } else
        {
            // trigger events for sanity depletion;
        }
    }

    public void Recover(float amount)
    {
        sanity = Mathf.Min(initialSanity, sanity + amount);
    }

    // Start is called before the first frame update
    void Start()
    {
        initialSanity = sanity;
    }

    // Update is called once per frame
    void Update()
    {
        sanity -= 0.001f * sanityLossSpeed;
    }
}
