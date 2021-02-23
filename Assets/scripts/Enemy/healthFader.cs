using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthFader: MonoBehaviour
{
    BossHealthBar bar;
    Slider slider;
    float fadeWaitTime = 1.0f;
    float shrinkSpeed = 0.4f;
    float currentFillAmount = 1f;
    float timer;

    float curHealth;
    float prevHealth;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        bar = FindObjectOfType<BossHealthBar>();
        prevHealth = bar.GetValue();
        timer = fadeWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        curHealth = bar.GetValue();
        if (prevHealth != curHealth)
        {
            timer = fadeWaitTime;
            prevHealth = curHealth;
        } 

        if(timer > 0)
        {
            timer -= Time.deltaTime; 
        } else
        {
            if (currentFillAmount >= bar.GetValue())
            {

                if (currentFillAmount - Time.deltaTime * shrinkSpeed >= bar.GetValue())
                {
                    currentFillAmount -= Time.deltaTime * shrinkSpeed;
                } else
                {
                    currentFillAmount = bar.GetValue();
                }
                slider.value = currentFillAmount;
            }
        }
    }
}
