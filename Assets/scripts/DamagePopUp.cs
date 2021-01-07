using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour { 

    [SerializeField] GameObject popUp;

    public void SetDamage(int damage)
    {
        popUp.GetComponent<TMP_Text>().text = damage.ToString(); 
    }
}
