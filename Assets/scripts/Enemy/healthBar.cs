using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    [SerializeField] Enemy AttachedEnemy;

    Vector3 localScale;
    float initialScale;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        initialScale = localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = AttachedEnemy.GetHealthPercentage() * initialScale;
        transform.localScale = localScale;
        Debug.Log(AttachedEnemy.GetHealthPercentage());
    }
}
