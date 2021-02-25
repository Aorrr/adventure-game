using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBloodthirster : Skill
{

    [Header("Skill Configuration")]
    [SerializeField] int UnlockLifeSteal;
    [SerializeField] int UpgradeLifeSteal;

    public override void TakeEffect()
    {
        if (!this.IfUnlocked())
        {
            stats.ModifyMeleeLifeSteal(UnlockLifeSteal);
        }
        else
        {
            stats.ModifyMeleeLifeSteal(UpgradeLifeSteal);
        }
    }
}
