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

    List<int> Textindex = new List<int>();

    void RandomPick()
    {
        StartText = Random.Range(0, TipTexts.Count);

        do
        {
            StartText = Random.Range(0, TipTexts.Count);
        } while (Textindex.IndexOf(StartText) != -1);

        Textindex.Add(StartText);

        if (Textindex.Count == TipTexts.Count)
        {
            Textindex.Clear();
        }
    }

    void TextViewNext()
    {
        TipText.text = TipTexts[StartText];      
    }

    private void Start()
    {
        StartText = Random.Range(0, TipTexts.Count);
        Textindex.Add(StartText);
        Debug.Log(StartText);
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
            Debug.Log(StartText);
        }
    }
}
