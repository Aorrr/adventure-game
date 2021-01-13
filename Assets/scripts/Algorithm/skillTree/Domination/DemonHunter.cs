using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonHunter : Skill
{
    [Header("Skill Configuration")]

    [SerializeField] int initialPene;
    [SerializeField] int upgradePene;

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
            stats.ModifyArmorPenetration(initialPene);
        }
        else
        {
            stats.ModifyArmorPenetration(upgradePene);
        }
    }
}
