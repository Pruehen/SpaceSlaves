using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipView : MonoBehaviour
{
    public List<string> TipTexts = new List<string>();
    int StartText;
    public TextMeshProUGUI TipText = null;

    float MCooltime = 3f;
    float DefaultMCooltime = 3f;

    public void RandomPick()
    {
        StartText = Random.Range(0, TipTexts.Count);
        Debug.Log(StartText);
    }

    public void TextViewNext()
    {
        TipText.text = TipTexts[StartText];      

        /*if(StartText == TipTexts.Count - 1)
         {
            StartText = 0;
         }
         else
         {
            StartText++;
         }*/
    }

    void Start()
    {
        RandomPick();
        TextViewNext();
    }

    private void Update()
    {
        MCool();
    }

    void MCool()
    {
        MCooltime -= Time.deltaTime;

        if (MCooltime <= 0)
        {
            MCooltime = DefaultMCooltime;
            RandomPick();
            TextViewNext();
        }
    }
}
