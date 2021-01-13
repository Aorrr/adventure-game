using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exp : MonoBehaviour
{
    [SerializeField] int expToNextLevel = 20;
    [SerializeField] float ExpIncreaseBtwnLvs = 1.2f;
    [SerializeField] int currentLevel = 1;
    [SerializeField] TMP_Text levelText;
    [SerializeField] int skillPtPerLevel = 1;
    // number of levels to increase skillPtPerLevel
    [SerializeField] int skillPtGainIncrement = 10;

    StatsManager stats;
    Slider slider;
    int currentExp;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        currentExp = 0;
        levelText.text = currentLevel.ToString();
        stats = FindObjectOfType<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)currentExp / (float)expToNextLevel;
    }

    public void GainExp(int amount)
    {
        if(amount < 0) { return; }
        if(expToNextLevel <= 0) { Debug.Log("Experience to the next level cannot be negative or zero"); }
        currentExp += amount;
        while(currentExp > expToNextLevel)
        {
            currentLevel++;
            currentExp -= expToNextLevel;
            Debug.Log("level: " + currentLevel + "current exp: "+ currentExp);
            expToNextLevel = (int)Mathf.Round((float)expToNextLevel * ExpIncreaseBtwnLvs);
            levelText.text = currentLevel.ToString();

            GainSkillPtn();
        }
    }

    public void GainSkillPtn()
    {
        stats.GainSkillPt(skillPtPerLevel + currentLevel/skillPtGainIncrement);
    }
}
