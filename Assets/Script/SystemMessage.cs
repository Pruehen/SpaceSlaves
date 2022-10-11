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
    bool IsFade = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void MessageManage()
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

    public void MessageQ(string Message)//�޽��� �ν��ϴ� ����
    {
        IsMessageIn = true;
        logText.text = Message;
    }
}
