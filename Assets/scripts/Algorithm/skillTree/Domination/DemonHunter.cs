using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonHunter : Skill
{
    [Header("Skill Configuration")]

    [SerializeField] int initialPene;
    [SerializeField] int upgradePene;

    public override void TakeEffect()
    {
        if (!this.IfUnlocked())
        {
            stats.ModifyArmorPenetration(initialPene);
        }
        else
        {
            stats.ModifyArmorPenetration(upgradePene);
        }
    }
}
