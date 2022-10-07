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
    SCV_MORE =2,
    SCV_AMOUNT_UP = 3,
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
    Dictionary<string, float> UpgradeTotal = new Dictionary<string, float>();
    // ���� �ִ� ����
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
        if (!UpgradeTotal.ContainsKey(type.ToString()))
        {
            RefreshUpgradeTotal(type);
        }  

        return UpgradeTotal[type.ToString()];
    }

    public void RefreshUpgradeTotal(UPGRADE_TYPE type) 
    {
        int id = (int)type * 1000;            
        int idx = id;
        float foundVal = 0;
        // �ش� ������ Ȱ��ȭ ���ִ��� üũ
        while (UpgradeActiveDict.ContainsKey(idx.ToString()))
        {
            foundVal += UpgradeStaticManager.instance.GetVal(idx);
            // ��Ż�� static���� ���� ���� ���Ѵ�.
            idx++;
            if (!UpgradeMaxId.TryGetValue(type, out int temp))
                UpgradeMaxId.Add(type, idx);
            else
                UpgradeMaxId[type] = idx;
        }
        // ��Ż�� static���� ���� ���� ���Ѵ�.
        UpgradeTotal.Add(type.ToString(), foundVal);
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

    public int GetBestUpgradeId(UPGRADE_TYPE type)
    {
        return UpgradeMaxId[type];
    }

    // ���׷��̵带 �Ѵ�.
    public void DoBestUpgrade(int idType)
    {
        var type = (UPGRADE_TYPE)idType;
        RefreshUpgradeTotal(type);
        int id = GetBestUpgradeId(type);
        DoUpgrade(id);
    }

    public void DoUpgrade(int id)
    {
        // ���Ἲ üũ
        if (true)
        {

        }  

        UpgradeActiveDict.Add(id.ToString(), true);
    }


    public void SaveData()
    {
        var data =  JsonConvert.SerializeObject(UpgradeActiveDict);
        
        File.WriteAllText(Application.dataPath + _saveFileName, data);
    }
    public void LoadData()
    {
        if (!File.Exists(Application.dataPath + _saveFileName))
            return;

        var fileData = File.ReadAllText(Application.dataPath + _saveFileName);
        var data = JsonConvert.DeserializeObject<Dictionary<string, bool>>(fileData);
        UpgradeActiveDict = data;
    }

    // ��ü ���۵� �׸��� ���, �׽�Ʈ���� 
    public void _ViewData()
    {
        var data = JsonConvert.SerializeObject(UpgradeActiveDict);
        Debug.Log(data);
    }


    private void Start()
    {
        LoadData();
    }
}
