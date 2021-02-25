using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [Header("Skill Information")]
    [SerializeField] string skillName;
    [SerializeField] [TextArea(3, 6)] public string description;

    [Header("Skill Setting")]
    [SerializeField] int maxLevel;
    [SerializeField] SkillTree tree;
    [SerializeField] Skill LowerlevelSkill;
    [SerializeField] int LowerSkillLvRequirement;

    [Header("Skill VFX")]
    [SerializeField] Image vertex;

    [Header("Skill Points")]
    [SerializeField] int UnlockSkillPoints = 10;
    [SerializeField] int LevelUpSkillPoints = 5;

    bool unlocked = false;
    public int currentLevel;
    protected StatsManager stats;

    // Update is called once per frame
    void Update()
    {
    }

    private void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        if (stats.IsUnlocked(skillName))
        {
            currentLevel = stats.GetSkillLevel(skillName);
            unlocked = true;
            GetComponent<Image>().color = Color.white;
            if (vertex != null)
            {
                vertex.color = Color.red;
            }
        }
        else
            SetColorToGrey();
    }

    public string GetName()
    {
        return skillName;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public string GetDesc()
    {
        if (CouldUnlock())
        {
            return description;
        }
        else
        {
            return GetAltText();
        }
    }

    public void LevelUp()
    {
        if (currentLevel == 0)
        {
            if (!CouldUnlock()) { return; }
            if (vertex != null)
            {
                vertex.color = Color.red;
            }
            GetComponent<Image>().color = Color.white;
        }

        if (currentLevel < maxLevel)
        {
            currentLevel++;
            TakeEffect();
        }
    }

    public Sprite GetImage()
    {
        return GetComponent<Image>().sprite;

    }

    public void SkillSelected()
    {
        tree.SelectSkill(this);
    }

    public bool IfUnlocked()
    {
        return unlocked;
    }

    public void ToggleUnlockStatus(bool status)
    {
        unlocked = status;
    }

    public abstract void TakeEffect();

    public int AmountOfSkillPtsNeeded()
    {
        if (!unlocked)
        {
            return UnlockSkillPoints;
        }
        else
        {
            return LevelUpSkillPoints;
        }
    }

    public void SetColorToGrey()
    {
        GetComponent<Image>().color = Color.grey;
        if (vertex != null)
        {
            vertex.color = Color.grey;
        }
    }


    public bool CouldUnlock()
    {
        if (LowerlevelSkill == null)
        {
            return true;
        }
        else
        {
            return LowerlevelSkill.GetCurrentLevel() >= LowerSkillLvRequirement;
        }
    }

    private string GetAltText()
    {
        string lockedText = "You are not yet ready for this power!\n\nREQUIREMENT: " +
            LowerlevelSkill.GetName() + " [" + LowerSkillLvRequirement + "]";
        return lockedText;
    }
}
