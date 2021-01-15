using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [Header("Melee Stats")]
    [SerializeField] int meleeDamage = 10;
    [SerializeField] int armor = 50;
    [SerializeField] int MeleeLifeSteal;

    [Header("Arrow Stats")]
    [SerializeField] int arrowDamage = 5;
    [SerializeField] int armorPenetration = 0;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowSpeed = 25f;


    [Header("Magical Stats")]
    [SerializeField] int magicalPower = 0;
    [SerializeField] int magicalResistance = 20;

    int skillPts = 100;
    int executionThreshold = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyMeleeDamage(int amount)
    {
        meleeDamage = Mathf.Max(0, meleeDamage += amount);
    }

    public void ModifyArrowDamage(int amount)
    {
        meleeDamage = Mathf.Max(0, arrowDamage += amount);
    }

    public void ModifyMagicalPower(int amount)
    {
        magicalPower = Mathf.Max(0, magicalPower += amount);
    }

    public void ModifyArmor(int amount)
    {
        armor = Mathf.Max(0, armor += amount);
    }

    public void ModifyMR(int amount)
    {
        magicalResistance = Mathf.Max(0, magicalResistance += amount);
    }

    public void CalculateFinalDamage(int amount, string type)
    {
        int resistance;
       switch(type)
        {
            case "physical": resistance = armor; return;

            case "magical": resistance = magicalResistance; return;

            case "true": resistance = 0; return;
        }
    }

    public void ModifyArmorPenetration(int amount)
    {
        armorPenetration = Mathf.Max(0, armorPenetration += amount);
    }


    public void ModifyMeleeLifeSteal(int amount)
    {
        MeleeLifeSteal = Mathf.Max(0, MeleeLifeSteal += amount);
    }

    public int GetSkillPtsRemaining()
    {
        return skillPts;
    }

    public void GainSkillPt(int amount)
    {
        if(amount < 0) { return;}
        skillPts += amount;
    }


    public void SpendSkillPt(int amount)
    {
        if(amount > skillPts)
        {
            Debug.Log("Not enough skillPts");
        } else
        {
            skillPts -= amount;
        }
    }


    /* settings in regards to arrows */
    public GameObject GetArrowPrefab()
    {
        return arrowPrefab;
    }

    public float GetArrowSpeed()
    {
        return arrowSpeed;
    }

    public void IncreaseArrowVelocity(int percent)
    {
        if (percent < 1) { return; }
        arrowSpeed += arrowSpeed * percent / 100;
    }

    public void IncreaseArrowDmg(int amount)
    {
        arrowDamage += amount;
        arrowPrefab.GetComponent<Arrow>().IncreaseDamage(amount);
    }

    /* Formulas for taking damage */
     
    public int TakePhysicalDamage(int IniDmg)
    {
        float reduction = (float)armor / ((float)armor + 100);
        reduction = 1 - reduction;
        return (int)Mathf.Round(reduction * IniDmg);

        // calculate maginal damage with MR
    }

    public int TakeMagicalDamage(int IniDmg)
    {
        float reduction = (float)magicalResistance / ((float)magicalResistance + 100);
        reduction = 1 - reduction;
        return (int)Mathf.Round(reduction * IniDmg);
    }

    public int GetExecutionThreshold() 
    {
        return executionThreshold;
    }

    public bool IfExecutionUnlocked()
    {
        return executionThreshold > 0;
    }

    public void IncreaseExecutionThreshold(int amount)
    {
        if(amount > 0)
        {
            executionThreshold += amount;
        }
    }

}

