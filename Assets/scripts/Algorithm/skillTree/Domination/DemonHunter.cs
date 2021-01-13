using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHunter : Skill
{
    [Header("Skill Configuration")]

    [SerializeField] int initialPene;
    [SerializeField] int upgradePene;

    StatsManager stats;
    private void Start()
    {
        stats = FindObjectOfType<StatsManager>();
    }

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
