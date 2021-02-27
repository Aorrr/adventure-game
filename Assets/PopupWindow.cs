using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}
