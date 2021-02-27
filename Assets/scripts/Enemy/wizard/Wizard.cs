using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Wizard : MonoBehaviour
{

    Animator amtr;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject king;
    bool hasCast = false;
    bool disappearing = false;
    // Start is called before the first frame update
    void Start()
    {
        circle.SetActive(false);
        amtr = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cast()
    {
        if(!hasCast)
        {
            circle.SetActive(true);
            hasCast = true;
            king.GetComponent<SkullKing>().Appear();
            amtr.SetTrigger("Cast");
        }
    }

    public void Disappear()
    {
        if(!disappearing)
        {
            amtr.SetTrigger("Fade");
            disappearing = true;
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
