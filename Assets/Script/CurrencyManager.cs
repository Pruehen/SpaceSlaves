using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
// ��ȭŸ��
public enum CURRENCY_TYPE
{
    Mineral = 1,
    Debri = 2
}

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    
    public TextMeshProUGUI minText;

    Button button; 

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        LoadCurrencyData();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InvokeRepeating("SaveCurrencyData", 60, 60);
        
    }

    public ref int GetCurrency(CURRENCY_TYPE type)
    {
        ref int cur = ref currencyData.minerals;

        switch (type)
        {
            case CURRENCY_TYPE.Mineral:
                cur = ref currencyData.minerals;
                break;
            case CURRENCY_TYPE.Debri:
                cur = ref currencyData.debris;
                break;
            default:
                break;
        }
        return ref cur;
    }

    

    // ȹ��
    public void AddCurrency(CURRENCY_TYPE type, int amount)
    {
        ref int currency = ref currencyData.minerals;
        currency = GetCurrency(type);
        currency += amount;
    }

    // ���
    public void CostCurrency(CURRENCY_TYPE type, int amount)
    { 
        ref int currency = ref currencyData.minerals;
        currency = GetCurrency(type);
        currency -= amount;
    }
    
    public bool CheckCurrency(CURRENCY_TYPE type, int amount)
    {
        ref int currency = ref currencyData.minerals;
        currency = GetCurrency(type);

        if (currency >= amount)
        {             
            currency -= amount;
            return true;
        }
        return false;
    }


    public string CurrencySaveDataFileName = "CurrencySaveData.json";
    CurrencyData currencyData = new CurrencyData();

    void LoadCurrencyData()
    {
        string filePath = Application.dataPath + CurrencySaveDataFileName;
        string FromJsonData = File.ReadAllText(filePath);
        currencyData = JsonConvert.DeserializeObject<CurrencyData>(FromJsonData);

        Debug.Log("��ȭ ������ �ҷ����� ����");
    }

    public void SaveCurrencyData()
    {
        string ToJsonData = JsonConvert.SerializeObject(currencyData);
        string filePath = Application.dataPath + CurrencySaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("��ȭ ������ ���� �Ϸ�");
    }

    void OnApplicationQuit()
    {
        SaveCurrencyData();
    }

    public class CurrencyData
    {
        public int minerals = 0;
        public int debris = 0;
    }
}
