using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemMessage : MonoBehaviour
{
    public GameObject SystemMessageUI;
    public TextMeshProUGUI logText = null;
    bool IsMessageIn = false;
    float MessageCool;
    private static SystemMessage instance;
    public TextMeshProUGUI textMassage;

    public static SystemMessage Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new SystemMessage();
            }
            return instance;
        }
    }

    public void selectYN()
    {
        if (IsMessageIn == true)
        {
            SystemMessageUI.gameObject.SetActive(true);
        }
    }

    public void MessageQ(string Message)
    {
        IsMessageIn = true;
        logText.text = Message;
    }
}
