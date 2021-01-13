using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    float sanRate;
    float lightRate;
    Light light;
    Sanity san;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        Time.timeScale = 0f;
        FreezeResource();
    }

    public void resume()
    {
        Time.timeScale = 1f;
        ReleasezeResource();
    }

    private void FreezeResource()
    {
        light = FindObjectOfType<Light>();
        san = FindObjectOfType<Sanity>();

        lightRate = light.GetFadeSpeed();
        sanRate = san.GetSanityLossSpeed();
        light.SetFadeSpeed(0);
        san.SetSanityLossSpeed(0);
    }

    private void ReleasezeResource()
    {
        light.SetFadeSpeed(lightRate);
        san.SetSanityLossSpeed(sanRate);
    }

    public void PopSkillTreeCanvas(bool state)
    {
        canvas.gameObject.SetActive(state);
    }
}
