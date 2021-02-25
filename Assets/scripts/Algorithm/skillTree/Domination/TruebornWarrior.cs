using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruebornWarrior : Skill
{
    [Header("Skill Configuration")]
    [SerializeField] int initialArmorBoost;
    [SerializeField] int levelArmorBoost;
    [SerializeField] int initialMeleeDmg;
    [SerializeField] int levelMeleeDmg;

    public override void TakeEffect()
    {
       if(!this.IfUnlocked())
        {
            stats.ModifyArmor(initialArmorBoost);
            stats.ModifyMeleeDamage(initialMeleeDmg);
        } else
        {
            stats.ModifyArmor(levelArmorBoost);
            stats.ModifyMeleeDamage(levelMeleeDmg);
        }
    }
}
