using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicPlayer : MonoBehaviour
{

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer mp = FindObjectOfType<MusicPlayer>();
        if (mp)
        {
            Destroy(mp.gameObject);
        }
            
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.8f;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

}
