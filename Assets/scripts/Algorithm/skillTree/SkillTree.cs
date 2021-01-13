using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [Header("Skill Display")]
    [SerializeField] Skill defaultSkill; // the currently selected skill for info display
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text descText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Image image;
    [SerializeField] Text confirmBtnTxt;

    Skill currentSkill;


    List<Skill> unlockedSkills;
    


    // Start is called before the first frame update
    void Start()
    {
        unlockedSkills = new List<Skill>();
        currentSkill = defaultSkill;
    }

    // Update is called once per frame
    void Update()
    {
        ShowCurrentSkillInfo();
    }

    public void SelectSkill(Skill skill)
    {
        currentSkill = skill;
    }

    public void ConfirmBtnPress()
    {
        currentSkill.LevelUp();
        if (!currentSkill.IfUnlocked())
        {
            AddToSkillList(currentSkill);
            currentSkill.ToggleUnlockStatus(true);
        } 
    }

    private void ShowCurrentSkillInfo()
    {
        if(currentSkill != null)
        {
            levelText.text = String.Format(currentSkill.GetCurrentLevel() + "/"
                + currentSkill.GetMaxLevel());

            descText.text = currentSkill.GetDesc();
            nameText.text = currentSkill.GetName();
            image.sprite = currentSkill.GetImage();

            if (currentSkill.IfUnlocked())
            {
                confirmBtnTxt.text = "LEVEL UP";
            }
            else
            {
                confirmBtnTxt.text = "UNLOCK";
            }
        }
    }

    public void AddToSkillList(Skill skill)
    {
        currentSkill = skill;
        if(!unlockedSkills.Contains(skill))
        {
            unlockedSkills.Add(skill);

        }
    }
}
