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

    [Header("Skill Points")]
    [SerializeField] TMP_Text skillPtsRemaining;
    [SerializeField] TMP_Text skillPtsNeeded;

    Skill currentSkill;
    StatsManager stats;


    List<Skill> unlockedSkills;



    // Start is called before the first frame update
    void Start()
    {
        currentSkill = defaultSkill;
        stats = FindObjectOfType<StatsManager>();
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
        if (!currentSkill.CouldUnlock() ||
            !(stats.GetSkillPtsRemaining() >= currentSkill.AmountOfSkillPtsNeeded()) ||
            currentSkill.GetCurrentLevel() >= currentSkill.GetMaxLevel()
            ) { return; }

        currentSkill.LevelUp();
        stats.SpendSkillPt(currentSkill.AmountOfSkillPtsNeeded());
        stats.SkillLevelUp(currentSkill);

        if (!currentSkill.IfUnlocked())
        {
            currentSkill.ToggleUnlockStatus(true);
        }
    }

    private void ShowCurrentSkillInfo()
    {
        if (currentSkill != null)
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
            skillPtsRemaining.text = String.Format("REMAINING POINTS: [" +
                stats.GetSkillPtsRemaining() + "]");

            skillPtsNeeded.text = String.Format("SKILL POINTS NEEDED: [" +
                currentSkill.AmountOfSkillPtsNeeded() + "]");

        }
    }
}
