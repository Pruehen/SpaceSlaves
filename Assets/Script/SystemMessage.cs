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

    private void Start()
    {
    }

    private void Update()
    {
        MessageDown();
    }

    void MessageDown()
    {
        scroll.verticalNormalizedPosition = 0.0f;
    }

    public void MessageQ(string Message)
    {
        logText.text += Message;
    }
}
