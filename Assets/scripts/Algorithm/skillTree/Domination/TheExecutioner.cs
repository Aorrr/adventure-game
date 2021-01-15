using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheExecutioner : Skill
{
    [Header("Skill Configuration")]
    [SerializeField] int UnlockLifeThreshold;
    [SerializeField] int UpgradeLifeThreshold;


    StatsManager stats;
    private void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        SetColorToGrey();
    }

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
