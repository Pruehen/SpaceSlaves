using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemMessage : MonoBehaviour
{
    private static SystemMessage instance;
    public TextMeshProUGUI textMassage; 
    Button button; 

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
        
    }

    public void MessageQ()
    {

    }

}
