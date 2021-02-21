using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summon : MonoBehaviour
{
    [SerializeField] Enemy toSummon;

    FireSkull summoned;
    // Start is called before the first frame update
    void Start()
    {
        summoned = (FireSkull)Instantiate(toSummon, transform.position, Quaternion.identity);
        summoned.BeStatic(2);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 4f);
    }
}
