using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public Animator transition;
    public float transition_time;

    public void StartLevel(int level)
    {
        StartCoroutine(LoadLevelWithCrossFade(level + 1));
    }

    public void LoadLevelSelection()
    {
        StartCoroutine(LoadLevelWithCrossFade(1));
    }

    public void LoadNewGame()
    {
        StatsManager stats = FindObjectOfType<StatsManager>();
        stats.ResetStats();
        LoadLevelSelection();
    }

    public void ContinueGame()
    {
        if (SaveSystem.LoadPlayer() == null)
        {
            PopupWindow[] windows = Resources.FindObjectsOfTypeAll<PopupWindow>();
            if (windows.Length > 0)
            {
                windows[0].gameObject.SetActive(true);
            }
        } 
        else
        {
            LoadLevelSelection();
        }
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelWithCrossFade(0));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadLevelWithCrossFade(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);
    }
}
