using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int gameLevel;

    public int playerLevel;
    public int currentExp;
    public int expToNextLevel;

    public int meleeDamage;
    public int armor;
    public int MeleeLifeSteal;

    public int arrowDamage;
    public int armorPenetration;
    public GameObject arrowPrefab;
    public float arrowSpeed;


    public int magicalPower;
    public int magicalResistance;

    public PlayerData (StatsManager stats)
    {
        gameLevel = stats.gameLevel;

        playerLevel = stats.playerLevel;
        currentExp = stats.currentExp;
        expToNextLevel = stats.expToNextLevel;

        meleeDamage = stats.meleeDamage;
        armor = stats.armor;
        MeleeLifeSteal = stats.MeleeLifeSteal;

        arrowDamage = stats.arrowDamage;
        armorPenetration = stats.armorPenetration;
        arrowPrefab = stats.arrowPrefab;
        arrowSpeed = stats.arrowSpeed;

        magicalPower = stats.magicalPower;
        magicalResistance = stats.magicalResistance;
    }
}
