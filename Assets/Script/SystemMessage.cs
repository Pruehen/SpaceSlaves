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

    //float MessageCool;
    private static SystemMessage instance;
    public TextMeshProUGUI textMassage; 

    float MessageCool = 1.5f;
    float defaultMessageCool = 1.5f;
    bool IsFade = false;
    public GameObject Log;

    private void Update()
    {
        //MessageManage();
        if(IsMessageIn == true && IsFade == true)
        {
            MassageCoolManage();
        }
    }

    public void MessageManage(string Message)//예/아니오 선택창
    { 
        if (IsMessageIn == true)
        {
            logText.text = Message;
        }
        else
        {
            SystemMessageUI.gameObject.SetActive(false);
        }    
    }

    public void MessageQ(string Message)//메시지 인식하는 역할
    {
        IsMessageIn = true;
        if (IsMessageIn == true)
        {
            logText.text = Message;
        }
    }

    public void MessageQFade(string Message)//메시지 인식하는 역할 // 몇초후 사라짐
    {
        IsMessageIn = true;
        IsFade = true;
        if(IsMessageIn == true && IsFade == true)
        {
            logText.text = Message;
        }
    }

    void MassageCoolManage()
    {
        MessageCool -= Time.deltaTime;
        if(MessageCool <= 0)
        {
            IsFade = false;
            IsMessageIn = false;
            MessageCool = defaultMessageCool;
        }
    }
}
