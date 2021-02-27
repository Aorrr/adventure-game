using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StatsManager : MonoBehaviour
{
    [Header("Game Stats")]
    public int gameLevel = 1;

    [Header("Level Stats")]
    public int playerLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel;

    [Header("Skills Stats")]
    public Dictionary<string, int> unlockedSkills;
    public int skillPts;

    [Header("Melee Stats")]
    public int meleeDamage = 10;
    public int armor = 50;
    public int MeleeLifeSteal;

    [Header("Arrow Stats")]
    public int arrowDamage = 5;
    public int armorPenetration = 0;
    public GameObject arrowPrefab;
    public float arrowSpeed = 25f;


    [Header("Magical Stats")]
    public int magicalPower = 0;
    public int magicalResistance = 20;

    int executionThreshold = 0;

    // Start is called before the first frame update
    void Start()
    {
        StatsManager[] sms = FindObjectsOfType<StatsManager>();
        unlockedSkills = new Dictionary<string, int>();
        LoadStatsData();
        if (sms.Length > 1)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    /* save & load stats data */
    public void LoadStatsData()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData == null)
            return;
        this.gameLevel = playerData.gameLevel;

        this.playerLevel = playerData.playerLevel;
        this.currentExp = playerData.currentExp;
        this.expToNextLevel = playerData.expToNextLevel;

        this.unlockedSkills = JsonConvert.DeserializeObject<Dictionary<string, int>>(playerData.unlockedSkills);
        this.skillPts = playerData.skillPts;

        this.meleeDamage = playerData.meleeDamage;
        this.armor = playerData.armor;
        this.MeleeLifeSteal = playerData.MeleeLifeSteal;

        this.arrowDamage = playerData.arrowDamage;
        this.armorPenetration = playerData.armorPenetration;
        this.arrowSpeed = playerData.arrowSpeed;

        this.magicalPower = playerData.magicalPower;
        this.magicalResistance = playerData.magicalResistance;

        return;
    }

    public void SaveStatsData()
    {
        SaveSystem.SavePlayer(this);
        return;
    }

    private void OnApplicationQuit()
    {
        SaveStatsData();
    }

    public void ResetStats()
    {
        gameLevel = 1;

        playerLevel = 1;
        currentExp = 0;
        expToNextLevel = 20;

        unlockedSkills = new Dictionary<string, int>();
        skillPts = 0;

        meleeDamage = 10;
        armor = 50;
        MeleeLifeSteal = 0;

        arrowDamage = 5;
        armorPenetration = 0;
        arrowSpeed = 25f;

        magicalPower = 0;
        magicalResistance = 20;
    }

    /* melee & magical stats */
    public void ModifyMeleeDamage(int amount)
    {
        meleeDamage = Mathf.Max(0, meleeDamage += amount);
    }

    public void ModifyArrowDamage(int amount)
    {
        meleeDamage = Mathf.Max(0, arrowDamage += amount);
    }

    public void ModifyMagicalPower(int amount)
    {
        magicalPower = Mathf.Max(0, magicalPower += amount);
    }

    public void ModifyArmor(int amount)
    {
        armor = Mathf.Max(0, armor += amount);
    }

    public void ModifyMR(int amount)
    {
        magicalResistance = Mathf.Max(0, magicalResistance += amount);
    }

    public void CalculateFinalDamage(int amount, string type)
    {
        int resistance;
        switch (type)
        {
            case "physical": resistance = armor; return;

            case "magical": resistance = magicalResistance; return;

            case "true": resistance = 0; return;
        }
    }

    public void ModifyArmorPenetration(int amount)
    {
        armorPenetration = Mathf.Max(0, armorPenetration += amount);
    }


    public void ModifyMeleeLifeSteal(int amount)
    {
        MeleeLifeSteal = Mathf.Max(0, MeleeLifeSteal += amount);
    }

    /* skills stats */
    public int GetSkillPtsRemaining()
    {
        return skillPts;
    }

    public void SpendSkillPt(int amount)
    {
        if (amount > skillPts)
        {
            Debug.Log("Not enough skillPts");
        }
        else
        {
            skillPts -= amount;
        }
    }

    public void SkillLevelUp(Skill skill)
    {
        if (!skill.IfUnlocked())
        {
            unlockedSkills.Add(skill.GetName(), 1);
            Debug.Log(unlockedSkills[skill.GetName()]);
        }
        else
        {
            unlockedSkills[skill.GetName()]++;
            Debug.Log(unlockedSkills[skill.GetName()]);
        }
    }

    public bool IsUnlocked(string skillName)
    {
        return unlockedSkills.ContainsKey(skillName);
    }

    public int GetSkillLevel(string skillName)
    {
        return unlockedSkills[skillName];
    }

    /* settings in regards to arrows */
    public GameObject GetArrowPrefab()
    {
        return arrowPrefab;
    }

    public float GetArrowSpeed()
    {
        return arrowSpeed;
    }

    public void IncreaseArrowVelocity(int percent)
    {
        if (percent < 1) { return; }
        arrowSpeed += arrowSpeed * percent / 100;
    }

    public void IncreaseArrowDmg(int amount)
    {
        arrowDamage += amount;
        arrowPrefab.GetComponent<Arrow>().IncreaseDamage(amount);
    }

    /* Formulas for taking damage */

    public int TakePhysicalDamage(int IniDmg)
    {
        float reduction = (float)armor / ((float)armor + 100);
        reduction = 1 - reduction;
        return (int)Mathf.Round(reduction * IniDmg);

        // calculate maginal damage with MR
    }

    public int TakeMagicalDamage(int IniDmg)
    {
        float reduction = (float)magicalResistance / ((float)magicalResistance + 100);
        reduction = 1 - reduction;
        return (int)Mathf.Round(reduction * IniDmg);
    }

    public int GetExecutionThreshold()
    {
        return executionThreshold;
    }

    public bool IfExecutionUnlocked()
    {
        return executionThreshold > 0;
    }

    public void IncreaseExecutionThreshold(int amount)
    {
        if (amount > 0)
        {
            executionThreshold += amount;
        }
    }

    public void GainExp(int amount)
    {
        currentExp += amount;
    }

    public void PlayerLevelUp(int skillPt, int exp, int nextExp)
    {
        if (skillPt < 0) { return; }
        skillPts += skillPt;
        playerLevel++;
        currentExp = exp;
        expToNextLevel = nextExp;
    }
} 