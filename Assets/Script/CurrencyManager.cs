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
    
    //public TextMeshProUGUI minText;

    Button button;

    string CurrencySaveDataFileName = "/data_currency_save.txt";
    CurrencyData currencyData = new CurrencyData();

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
        OffCurrencyData(); 
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
        ref int currency = ref GetCurrency(type);
        currency += amount;

        //LaborSceneManager.instance.SetMinUI();
    }

    // ���
    public void CostCurrency(CURRENCY_TYPE type, int amount)
    { 
        ref int currency = ref GetCurrency(type);
        currency -= amount;

        //LaborSceneManager.instance.SetMinUI();
    }
    
    public bool CheckCurrency(CURRENCY_TYPE type, int amount)
    {
        ref int currency = ref GetCurrency(type);

        if (currency >= amount)
        {
            CostCurrency(type, amount);
            //LaborSceneManager.instance.SetMinUI();
            return true;
        }
        return false;
    }    

    void LoadCurrencyData()
    {
        if (!File.Exists(Application.dataPath + CurrencySaveDataFileName))
            return;
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

    void OffCurrencyData()
    {

        Debug.Log((int)Time.realtimeSinceStartup); 
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
