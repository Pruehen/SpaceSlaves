using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// 재화타입
public enum CURRENCY_TYPE
{
    Mineral = 1,
    Debri = 2
}

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    
    public TextMeshProUGUI minText; 

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

    int minerals = 0;
    int debris = 0;
    public ref int GetCurrency(CURRENCY_TYPE type)
    {
        ref int cur = ref minerals;

        switch (type)
        {
            case CURRENCY_TYPE.Mineral:
                cur = ref minerals;
                break;
            case CURRENCY_TYPE.Debri:
                cur = ref debris;
                break;
            default:
                break;
        }
        return ref cur;
    }

    public void AddCurrency(CURRENCY_TYPE type, int amount)
    {
        ref int currency = ref minerals;
        currency = GetCurrency(type);
        currency += amount;
    }


    public bool CostCurrency(CURRENCY_TYPE type, int amount)
    {
        ref int currency = ref minerals;
        currency = GetCurrency(type);

        if (currency >= amount)
        {             
            currency -= amount;
            return true;
        }
        return false;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("mtx", int.Parse(minText.text)); 
    }
    public void LoadData()
    {      
        if(PlayerPrefs.HasKey("mtx"))
        {
            minText.text = PlayerPrefs.GetInt("mtx").ToString(); 
        }
    }
}
