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
<<<<<<< HEAD
    float MessageCool;
    private static SystemMessage instance;
    public TextMeshProUGUI textMassage; 
=======
    float MessageCool = 1.5f;
    float defaultMessageCool = 1.5f;
    bool IsFade = false;
    public GameObject Log;
>>>>>>> 24dcc12bab1976f23b86a6a3f9e1ddfbaaf25884

    private void Update()
    {
        MessageManage();
        if(IsMessageIn == true && IsFade == true)
        {
            MassageCoolManage();
        }
    }

    public void SelectYes(string Message)
    {
<<<<<<< HEAD
        if(IsMessageIn == true)
        {
            SystemMessageUI.gameObject.SetActive(true);
        }
    }

    public void MessageQ(string Message)
    {
        IsMessageIn = true;
        logText.text = Message;
    }
=======
        IsMessageIn = false;
        Log.SendMessage(Message);
    }

    public void Check()//Ȯ��â
    {
        IsMessageIn = false;
    }

    public void MessageManage()//��/�ƴϿ� ����â
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

    public void MessageQ(string Message)//�޽��� �ν��ϴ� ����
    {
        IsMessageIn = true;
        if (IsMessageIn == true)
        {
            logText.text = Message;
        }
    }

    public void MessageQFade(string Message)//�޽��� �ν��ϴ� ���� // ������ �����
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
>>>>>>> 24dcc12bab1976f23b86a6a3f9e1ddfbaaf25884
}
