using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityController: MonoBehaviour
{
    Slider slider;
    Sanity sanity;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        sanity = FindObjectOfType<Sanity>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = sanity.GetSanity() / sanity.GetInitialSanity();
    }
}
