using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Sanity : MonoBehaviour
{
    [SerializeField] float sanity;
    [SerializeField] float sanityLossSpeed;
    [SerializeField] Light light;

    Light2D light2d;
    float initialSanity;
    bool invulnerable = false;

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

    public void ToggleInvulnerability(bool invulState)
    {
        invulnerable = invulState;
    }
    public void LoseSanity(float amount)
    {
        if(invulnerable) { return; }
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
        light2d = light.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(light2d.pointLightOuterRadius <= light.GetMinimalRadius())
            sanity -= Time.deltaTime * sanityLossSpeed;
        if(sanity <= 0)
        {
            FindObjectOfType<Player>().Die();
        }
    }
}
