
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] string[] keyName;
    [SerializeField] Text instruction;

    Animator myAnimator;

    Player player;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        instruction.gameObject.SetActive(true);
        instruction.text = "Press tab to open the door";
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        Open(player);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        instruction.gameObject.SetActive(false);
    }

    public void Open(Player player)
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(HasKey(player))
            {
                myAnimator.SetTrigger("open");
            } else
            {
                string text = "You need ";
                for(int i = 0; i < keyName.Length; i++)
                {
                        text += keyName[i] + " ";
                }
                text += "to open this door";
                instruction.text = text;
            }
        }
    }

    private bool HasKey(Player player)
    {
        foreach(string key in keyName)
        {
            if (!player.HasItem(key))
            {
                return false;
            }
        }
        return true;
    }
}
