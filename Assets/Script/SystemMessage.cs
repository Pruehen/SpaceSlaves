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
    GameObject Log;

    public void Start()
    {
        if (MessageManager.instance == null)
            return;
        MessageManager.instance.SetGoMessage(this);
    }
    public void OnDestroy()
    {
        if (MessageManager.instance == null)
            return;
        MessageManager.instance.RemoveGoMessage(this);
    }

    private void Update()
    {
        if (SystemMessageUI == null)
        {
            return;
        }

        MessageManage();
        if(IsMessageIn == true && IsFade == true)
        {
            MassageCoolManage();
        }
    }

    public void _SetLog(GameObject gameObject)
    {
        Log = gameObject;
    }

    public void SelectYes(string Message)//확인 후 실행
    {
        IsMessageIn = false;
        if (Message != "")
        {
            Log.SendMessage(Message);
        }
    }
    public void SelectNo(string Message)//확인 후 실행
    {
        IsMessageIn = false;
        if (Message != "")
        {
            Log.SendMessage(Message);
        }
    }

    public void Check()//확인창
    {
        IsMessageIn = false;
    }

    public void MessageManage()// 창 띄우는거
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
        if(IsMessageIn == true && IsFade == true)
        {
            logText.text = Message;
        }
    }

    void MassageCoolManage() //사라지는 창 쿨
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
