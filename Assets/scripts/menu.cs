using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public Animator transition;
    public float transition_time;
    public void StartFirstLevel()
    {
        StartCoroutine(LoadLevelWithCrossFade(2));
    }

    public void LoadLevelSelection()
    {
        StartCoroutine(LoadLevelWithCrossFade(1));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelWithCrossFade(0));
    }

    IEnumerator LoadLevelWithCrossFade(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(index);
    }
}
