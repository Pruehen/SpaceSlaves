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
    float MessageCool = 1.5f;
    float defaultMessageCool = 1.5f;
    bool IsFade = false;
    public GameObject Log;

    private void Update()
    {
        MessageManage();
        if (IsMessageIn == true && IsFade == true)
        {
            MassageCoolManage();
        }
    }

    public void SelectYes(string Message)//확인 후 실행
    {
        IsMessageIn = false;
        Log.SendMessage(Message);
    }

    public void Check()//확인창
    {
        IsMessageIn = false;
    }

    public void MessageManage()// 메시지 띄우는..
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
        if (IsMessageIn == true)
        {
            logText.text = Message;
        }
    }

    public void MessageQFade(string Message)//메시지 인식하는 역할 // 몇초후 사라짐
    {
        IsMessageIn = true;
        IsFade = true;
        if (IsMessageIn == true && IsFade == true)
        {
            logText.text = Message;
        }
    }

    void MassageCoolManage()
    {
        MessageCool -= Time.deltaTime;
        if (MessageCool <= 0)
        {
            IsFade = false;
            IsMessageIn = false;
            MessageCool = defaultMessageCool;
        }
    }
}
