using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] int meleeDamage;
    [SerializeField] int arrowDamage;
    [SerializeField] int magicalPower;
    [SerializeField] int armor;
    [SerializeField] int magicalResistance;
    [SerializeField] int armorPenetration;
    [SerializeField] int MeleeLifeSteal;

    int skillPts = 100;

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

    public bool SpendSkillPt(int amount)
    {
        if(amount > skillPts)
        {
            Debug.Log("Not enough skillPts");
            return false;
        } else
        {
            skillPts -= amount;
            return true;
        }
    }
}

