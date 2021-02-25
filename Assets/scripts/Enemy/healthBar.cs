using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    [SerializeField] Enemy AttachedEnemy;
    [SerializeField] GameObject Hpbar;

    Vector3 localScale;
    float initialScale;
    Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        initialScale = localScale.x;
        rotation = Hpbar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = AttachedEnemy.GetHealthPercentage() * initialScale;

        if(AttachedEnemy.transform.localScale.x < 0)
        {
            rotation.x = -Mathf.Abs(rotation.x);
            Hpbar.transform.localScale = rotation;
        } else
        {
            rotation.x = Mathf.Abs(rotation.x);
            Hpbar.transform.localScale = rotation;
        }
        transform.localScale = localScale;
    }
}
