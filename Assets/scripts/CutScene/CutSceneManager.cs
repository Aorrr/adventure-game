using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    bool cutSceneStart = false;
    [SerializeField] TextDisplay display;
    Wizard wizard;
    [SerializeField] GameObject system;
    [SerializeField] GameObject textDis;
    bool ifDone = false;
    // Start is called before the first frame update
    void Start()
    {
        wizard = FindObjectOfType<Wizard>();
        system.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!cutSceneStart) { return; }
        display.StartDisplay();

        if(display.IfFinished() && !ifDone)
        {
            wizard.cast();
            StartCoroutine(WizardDisappearTimer());
            ifDone = !ifDone;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<CameraControl>().EnterCutScene();
            cutSceneStart = true;
        }
    }

    IEnumerator WizardDisappearTimer()
    {
        yield return new WaitForSeconds(4);
        wizard.Disappear();
        system.SetActive(true);
        display.SwitchText("Enjoy.");
        StartCoroutine(TransitOutOfCutScene());
    }

    IEnumerator TransitOutOfCutScene()
    {
        yield return new WaitForSeconds(4);
        FindObjectOfType<CameraControl>().OutOfCutScene();
        Destroy(system);
        wizard.DestroyThis();
        Destroy(gameObject);
    }
}
