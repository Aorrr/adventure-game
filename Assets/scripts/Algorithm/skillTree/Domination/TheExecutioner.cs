using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheExecutioner : Skill
{
    [Header("Skill Configuration")]
    [SerializeField] int UnlockLifeThreshold;
    [SerializeField] int UpgradeLifeThreshold;

    public override void TakeEffect()
    {
        if (!this.IfUnlocked())
        {
            stats.IncreaseExecutionThreshold(UnlockLifeThreshold);
        }
        else
        {
            stats.IncreaseExecutionThreshold(UpgradeLifeThreshold);
        }
    }
}
