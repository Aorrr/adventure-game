using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summon : MonoBehaviour
{
    [SerializeField] Enemy toSummon;
    [SerializeField] GameObject SummonIn;
    

    FireSkull summoned;
    // Start is called before the first frame update
    void Start()
    {
        if(toSummon == null) { return; }
     
        summoned = (FireSkull)Instantiate(toSummon, transform.position, Quaternion.identity);
        if(SummonIn)
        {
            summoned.transform.parent = SummonIn.transform;
        }
        summoned.BeStatic(2);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 4f);
    }
}
