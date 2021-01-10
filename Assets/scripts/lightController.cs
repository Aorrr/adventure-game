using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class lightController : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    Light light;
    void Start()
    {
        slider = GetComponent<Slider>();
        light = FindObjectOfType<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (light.GetRadius() - light.GetMinimalRadius()) / (light.GetInitialRadius() - light.GetMinimalRadius());
    }
}