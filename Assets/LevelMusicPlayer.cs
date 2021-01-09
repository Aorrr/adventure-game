using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicPlayer : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip BossBGM;

    BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {       
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.8f;
        box = FindObjectOfType<bossSceneTrigger>().GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(box.IsTouchingLayers(LayerMask.GetMask("Player"))) { SwitchSong(); }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SwitchSong()
    {
        if(audioSource.clip != BossBGM)
        {
            Debug.Log("Switching!!");
            audioSource.clip = BossBGM;
            audioSource.Play();
        }
    }


    // used to fade out song
    // from https://gamedevbeginner.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
