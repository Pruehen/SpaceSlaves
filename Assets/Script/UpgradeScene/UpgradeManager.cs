using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
[Serializable]
public enum UPGRADE_TYPE 
{
    // �������� ����
    NONE = 0,
    SCV_SPEED_UP =1,
    SCV_AMOUNT_UP = 2,
    COLLECTOR_CAPA = 3,
    FLEET = 4,
}


// ������ ������ ���׷��̵带 ����ϰ� �����ϰ� �ִ�
public class UpgradeManager : MonoBehaviour
{
    public static  UpgradeManager instance;

    string _saveFileName = "/data_upgrade_prog.json";

    // id ��Ģ 
    // 1000 ������ ī�װ� ����

    //ex. 1000���� 1999������ XXX�� ���׷��̵��.
    //3000���� 3999������ YYY�� ���׷��̵��.
    //1000~1999 ���̿� ��ȿ�� ���׷��̵���� ���ӵ� �ε����� ����ؾ��Ѵ�.

    //ex 1020�� ���� �ܰ� ���׷��̵�� 1021�̿��� �Ѵ�.
    //ex 1030�� ���� �ܰ� ���׷��̵尡 1039�ϼ��� ����. 

    [SerializeField]
    // "id / Ȱ��ȭ��"  ���� ������ ��ųʸ�
    Dictionary<string, bool> UpgradeActiveDict = new Dictionary<string, bool>();
    // ���׷��̵� ������ ��� �ִ� ��ųʸ�
    Dictionary<UPGRADE_TYPE, float> UpgradeTotal = new Dictionary<UPGRADE_TYPE, float>();
    // ���� ���� ����
    Dictionary<UPGRADE_TYPE, int> UpgradeMaxId = new Dictionary<UPGRADE_TYPE, int>();

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

    public float GetTotalActiveVal(UPGRADE_TYPE type)
    {
        int id = (int)type * 1000;
        if (!UpgradeTotal.ContainsKey(type))
        {
            RefreshUpgradeTotal(type);
        }  

        return UpgradeTotal[type];
    }
    // �������ִ� ����, �������ִ� ���� �ջ�, ���� ���� �ܰ� ��������
    public void RefreshUpgradeTotal(UPGRADE_TYPE type) 
    {
        int id = (int)type * 1000;            
        int idx = id;
        float foundVal = 0;

        // �ش� ������ Ȱ��ȭ ���ִ��� üũ
        while (UpgradeActiveDict.ContainsKey(idx.ToString()))
        {
            foundVal += UpgradeStaticManager.instance.GetVal(idx);            
            idx++;
        }

        // ��Ż�� static���� ���� ���� ���Ѵ�.
        UpgradeTotal.Add(type, foundVal);
    }

    //check ����
    public bool CheckUpgradeable(string id)
    {
        bool val;
        if (UpgradeActiveDict.TryGetValue(id, out val))
        {
            return val;
        }
        return true;
    }

    // ���� �ְ� ���׷��̵� ��ȯ
    public int GetBestUpgradeId(UPGRADE_TYPE type)
    {
        if (UpgradeMaxId.ContainsKey(type))
            return UpgradeMaxId[type];
        return (int)type * 1000;
    }

    // ���׷��̵带 �Ѵ�. 
    // ������ ����� ���� �Ķ���ʹ� int�� 
    public void DoBestUpgrade(int idType)
    {
        var type = (UPGRADE_TYPE)idType;
        _DoUpgrade(GetBestUpgradeId(type));
    }

    void _DoUpgrade(int id, bool isLoading = false)
    {
        int cost = UpgradeStaticManager.instance.GetCost(id);
        if (!isLoading)
        {
            if (CurrencyManager.instance.CheckCurrency(CURRENCY_TYPE.Debri, cost))
            {
                CurrencyManager.instance.CostCurrency(CURRENCY_TYPE.Debri, cost);
            }
            else  // ������ �����϶�
            {
                Debug.Log("������ / " + cost + " �ʿ�");
                return;
            }
        }

        // ���� ���׷��̵�� ���� �� �� ����.
        if (!UpgradeStaticManager.instance.IsExist(id))
            return;

        // ���� ��� �ܰ� ���
        var type = (UPGRADE_TYPE)((int)id / 1000);
        if (UpgradeMaxId.ContainsKey(type))
            UpgradeMaxId[type] = Math.Max(UpgradeMaxId[type], id + 1);
        else
            UpgradeMaxId.Add(type, id + 1);

        // ���� ���
        if (UpgradeTotal.ContainsKey(type))
            UpgradeTotal[type] += UpgradeStaticManager.instance.GetVal(id);
        else
            UpgradeTotal.Add(type, UpgradeStaticManager.instance.GetVal(id));

        // �� ������ Ȱ��ȭ
        if (!UpgradeActiveDict.ContainsKey(id.ToString()))
        {
            UpgradeActiveDict.Add(id.ToString(), true);
        }

        Debug.Log(id.ToString() + " upgraded");
    }


    public void SaveData()
    {
        var data =  JsonConvert.SerializeObject(UpgradeActiveDict);
        
        File.WriteAllText(Application.dataPath + _saveFileName, data);

        Debug.Log("���׷��̵� ������ ���� �Ϸ�");
    }
    public void LoadData()
    {
        if (!File.Exists(Application.dataPath + _saveFileName))
            return;

        var fileData = File.ReadAllText(Application.dataPath + _saveFileName);
        var data = JsonConvert.DeserializeObject<Dictionary<string, bool>>(fileData);
        UpgradeActiveDict = data;

        foreach (var item in UpgradeActiveDict.Keys)
            _DoUpgrade(int.Parse(item), true);

        Debug.Log("���׷��̵� ������ �ҷ����� �Ϸ�");
    }

    // ��ü ���۵� �׸��� ���, �׽�Ʈ���� 
    public void _ViewData()
    {
        var data = JsonConvert.SerializeObject(UpgradeActiveDict);
        Debug.Log(data);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void Start()
    {
        LoadData();
    }
}
