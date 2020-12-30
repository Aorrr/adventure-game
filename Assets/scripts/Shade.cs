using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shade : MonoBehaviour
{
    // Start is called before the first frame update
    Image shade;
    Light light;
    void Start()
    {
        shade = GetComponent<Image>();
        light = FindObjectOfType<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        shade.fillAmount = 1 - light.GetRadius() / 2.5f;
    }
}
