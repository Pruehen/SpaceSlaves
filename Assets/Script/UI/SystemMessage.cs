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
    public Image Fade;

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
        SoundManager.instance.clickSoundOn();
        if (Message != "")
        {
            Log.SendMessage(Message);
        }
    }
    public void SelectNo(string Message)//확인 후 실행
    {
        IsMessageIn = false;
        SoundManager.instance.clickSoundOn();
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

    public void MessageQ(string Message)//메시지 받는 역할
    {
        IsMessageIn = true;
        if (IsMessageIn == true)
        {
            logText.text = Message;
        }
    }

    public void MessageQFade(string Message)//메시지 받는 역할 // 몇초후 사라짐
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
        Color color = Fade.color;

        MessageCool -= Time.deltaTime;
        if(MessageCool <= 0)
        {
            IsFade = false;
            IsMessageIn = false;
            MessageCool = defaultMessageCool;

            // 꺼질때는 알파값 원상 복귀가 필요하다.
            logText.color = new Color(logText.color.r, logText.color.g, logText.color.b, 1);
            color.a = 1;
            Fade.color = color;
            logText.color = Color.white;
        }
        else if(MessageCool <= 1)
        {
            logText.color = new Color(logText.color.r, logText.color.g, logText.color.b, logText.color.a - Time.deltaTime / defaultMessageCool);

            color.a -= Time.deltaTime / defaultMessageCool;
            Fade.color = color;
        }
    }
}
