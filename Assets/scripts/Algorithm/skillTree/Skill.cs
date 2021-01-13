using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [Header("Skill Information")]
    [SerializeField] string skillName;
    [SerializeField] [TextArea(3,6)] public string description;

    [Header("Skill Setting")]
    [SerializeField] int maxLevel;
    [SerializeField] SkillTree tree;

    [Header("Skill VFX")]
    [SerializeField] Image vertex;

    bool unlocked = false;
    int currentLevel = 0;

    // Update is called once per frame
    void Update()
    {
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
        return description;
    }

    public void LevelUp()
    {
        if(currentLevel == 0)
        {
            if(vertex!=null)
            {
                vertex.color = Color.red;
            }
            GetComponent<Image>().color = Color.white;
        }

        if(currentLevel < maxLevel)
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
}
