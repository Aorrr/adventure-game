using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    StatsManager stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
    }

    public void SaveGame()
    {
        stats.SaveStatsData();
    }

    public void LoadGame()
    {
        stats.LoadStatsData();
    }
}
