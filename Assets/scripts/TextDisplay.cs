using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{

    [SerializeField] float displayIterval = 0.5f;
    [SerializeField] int maxDisplay = 20;
    [SerializeField] [TextArea] string text;
    [SerializeField] GameObject obj;

    private string displayedText = "";
    Text textBox;
    char[] array;
    int iterator = 0;
    float timer = 0;
    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<Text>();
        array = text.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(iterator >= array.Length -1 || !start) {
            obj.SetActive(false);
            displayedText = "";
                     return; }
        if(timer < displayIterval)
        {
            timer += Time.deltaTime;
        } else
        {
            if(iterator % maxDisplay == 0)
            {
                displayedText = "";
            }
            timer = 0;
  
            displayedText += array[iterator];
            iterator += 1;

            textBox.text = displayedText;
        }
    }

    public void StartDisplay()
    {
        start = true;
        obj.SetActive(true);
    }

    public bool IfFinished()
    {
        return iterator >= array.Length - 1;
    }

    public void SwitchText(string newText)
    {
        textBox.text = "";
        obj.SetActive(true);
        text = newText;
        timer = 0;
        array = text.ToCharArray();
        iterator = 0;
        start = false;
    }
}
