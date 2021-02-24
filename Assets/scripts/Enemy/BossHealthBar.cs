﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Enemy boss;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!slider)
        {
            Start();
        }
        slider.value = boss.GetHealthPercentage();
    }

    public float GetValue()
    {
        return slider.value;
    } 
}
