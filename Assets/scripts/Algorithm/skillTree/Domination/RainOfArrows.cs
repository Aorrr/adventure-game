using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOfArrows : Skill
{
    [Header("Unlock Stat")]
    [SerializeField] int UnlockArrowSpeedBoost;
    [SerializeField] int UnlockArrowDmg;

    [Header("Upgrade Stat")]
    [SerializeField] int UpgradeArrowSpeedBoost;
    [SerializeField] int UpgradeArrowDmg;

    public override void TakeEffect()
    {
        if (!this.IfUnlocked())
        {
            stats.IncreaseArrowDmg(UnlockArrowDmg);
            stats.IncreaseArrowVelocity(UnlockArrowSpeedBoost);
        }
        else
        {
            stats.IncreaseArrowDmg(UpgradeArrowDmg);
            stats.IncreaseArrowVelocity(UpgradeArrowSpeedBoost);
        }
    }
}
