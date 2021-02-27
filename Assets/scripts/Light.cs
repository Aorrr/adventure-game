using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Light : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float fadeSpeed;
    [SerializeField] float minimalRadius = 0.3f;
    float initialRadius;
    Light2D parameters;

    public float GetRadius()
    {
        return parameters.pointLightOuterRadius;
    }

    public void SetFadeSpeed(float amount)
    {
        fadeSpeed = amount;
    }

    public float GetInitialRadius()
    {
        return initialRadius;
    }

    public float GetMinimalRadius()
    {
        return minimalRadius;
    }

    public float GetFadeSpeed()
    {
        return fadeSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
       // parameters.pointLightOuterRadius = outerRadius;
        transform.position = player.transform.position;
        parameters = GetComponent<Light2D>();
        initialRadius = parameters.pointLightOuterRadius;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lightPos = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = lightPos;
        if(parameters.pointLightOuterRadius > minimalRadius)
        {
            parameters.pointLightInnerRadius -= fadeSpeed * Time.deltaTime;
            parameters.pointLightOuterRadius -= fadeSpeed * Time.deltaTime;
        }
            
    }

    public void changeRadius(float amount)
    {
        parameters.pointLightOuterRadius = Mathf.Max(0, parameters.pointLightOuterRadius += amount);
        if(parameters.pointLightOuterRadius > initialRadius)
        {
            initialRadius = parameters.pointLightOuterRadius;
        }
    }
}