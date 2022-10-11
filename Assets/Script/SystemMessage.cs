using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessage : MonoBehaviour
{
    public ScrollRect scroll = null;
    public TextMeshProUGUI logText = null;
    bool IsMessageIn = false;
    float MessageCool;

    private void Start()
    {

    }

    private void Update()
    {
        MessageDown();

        if(IsMessageIn == true)
        {

        }
    }

    void MessageDown()
    {
        scroll.verticalNormalizedPosition = 0.0f;
    }

    public void MessageQ(string Message)
    {
        IsMessageIn = true;
        logText.text += Message;
    }
}
