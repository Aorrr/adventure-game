using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DamagePopUp : MonoBehaviour { 

    [SerializeField] GameObject popUp;

    public void SetDamage(int damage)
    {
        popUp.GetComponent<TMP_Text>().text = damage.ToString(); 
    }

    public void SetText(String text)
    {
        popUp.GetComponent<TMP_Text>().text = text;
    }
}
