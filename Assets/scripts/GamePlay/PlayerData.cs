using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerData
{
    public int gameLevel;

    public int playerLevel;
    public int currentExp;
    public int expToNextLevel;

    public string unlockedSkills;
    public int skillPts;

    public int meleeDamage;
    public int armor;
    public int MeleeLifeSteal;

    public int arrowDamage;
    public int armorPenetration;
    public float arrowSpeed;


    public int magicalPower;
    public int magicalResistance;

    int executionThreshold = 0;

    public PlayerData (StatsManager stats)
    {
        gameLevel = stats.gameLevel;

        playerLevel = stats.playerLevel;
        currentExp = stats.currentExp;
        expToNextLevel = stats.expToNextLevel;

        unlockedSkills = JsonConvert.SerializeObject(stats.unlockedSkills, Formatting.Indented);
        skillPts = stats.skillPts;

        meleeDamage = stats.meleeDamage;
        armor = stats.armor;
        MeleeLifeSteal = stats.MeleeLifeSteal;

        arrowDamage = stats.arrowDamage;
        armorPenetration = stats.armorPenetration;
        arrowSpeed = stats.arrowSpeed;

        magicalPower = stats.magicalPower;
        magicalResistance = stats.magicalResistance;
    }
}
