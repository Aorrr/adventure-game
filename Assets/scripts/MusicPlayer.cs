using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer[] mps = FindObjectsOfType<MusicPlayer>();
        if (mps.Length > 1)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(this);
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.8f;
        }  
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

}
