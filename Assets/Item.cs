using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] string itemName;
    [SerializeField] GameObject pickupEffect;
    [SerializeField] float animationDuration;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        player.ReceiveItem(itemName);
        GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(effect, animationDuration);
        Destroy(gameObject);
    }
}
