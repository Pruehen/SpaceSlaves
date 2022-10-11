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
    //public TextMeshProUGUI textMassage;

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

    public void SelectYN()
    {
        
        
        if (IsMessageIn == true)
        {
            SystemMessageUI.gameObject.SetActive(true);
        }
        else
        {
            SystemMessageUI.gameObject.SetActive(false); 
        }    
    }

    public void MessageQ(string Message)//메시지 인식하는 역할
    {
        IsMessageIn = true;
        logText.text = Message;
    }
}
