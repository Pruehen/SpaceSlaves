using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    SystemMessage goMessageOk;
    public static MessageManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }
    public void SetGoMessage(SystemMessage go)
    {
        goMessageOk = go;
    }
    public void RemoveGoMessage(SystemMessage go)
    {
        goMessageOk = null;
    }    

    public void PopupOk(string text, GameObject caller = null, string methodName = "")
    {
        goMessageOk.MessageQ(text);
        goMessageOk._SetLog(caller);
    }
}
