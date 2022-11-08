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

    public void SelectYes(string Message)//Ȯ�� �� ����
    {
        IsMessageIn = false;
        SoundManager.instance.clickSoundOn();
        if (Message != "")
        {
            Log.SendMessage(Message);
        }
    }
    public void SelectNo(string Message)//Ȯ�� �� ����
    {
        IsMessageIn = false;
        SoundManager.instance.clickSoundOn();
        if (Message != "")
        {
            Log.SendMessage(Message);
        }
    }

    public void Check()//Ȯ��â
    {
        IsMessageIn = false;
    }

    public void MessageManage()// â ���°�
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

    public void MessageQ(string Message)//�޽��� �޴� ����
    {
        IsMessageIn = true;
        if (IsMessageIn == true)
        {
            logText.text = Message;
        }
    }

    public void MessageQFade(string Message)//�޽��� �޴� ���� // ������ �����
    {
        IsMessageIn = true;
        IsFade = true;
        if(IsMessageIn == true && IsFade == true)
        {
            logText.text = Message;
        }
    }

    void MassageCoolManage() //������� â ��
    {
        Color color = Fade.color;

        MessageCool -= Time.deltaTime;
        if(MessageCool <= 0)
        {
            IsFade = false;
            IsMessageIn = false;
            MessageCool = defaultMessageCool;

            // �������� ���İ� ���� ���Ͱ� �ʿ��ϴ�.
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
