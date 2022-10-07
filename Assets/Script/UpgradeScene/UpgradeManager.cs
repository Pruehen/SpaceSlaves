using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public enum UPGRADE_TYPE 
{
    // �������� ����
    SCV_MORE =1,
    SCV_SPEED_UP =2,
    SCV_AMOUNT_UP = 3,
}


// ������ ������ ���׷��̵带 ����ϰ� �����ϰ� �ִ�
public class UpgradeManager : MonoBehaviour
{
    string _saveFileName = "/upgrade_prog_data.json";

    public static  UpgradeManager instance;

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

    Dictionary<string, int> UpgradeTotal = new Dictionary<string, int>();

    public float GetTotalActiveVal(UPGRADE_TYPE type)
    {
        int id = (int)type * 1000;
        if (!UpgradeTotal.ContainsKey(type.ToString()))
        { 
            int idx = id;
            // �ش� ������ Ȱ��ȭ ���ִ��� üũ
            while (UpgradeActiveDict.ContainsKey(idx.ToString()))
            {
                // ��Ż�� static���� ���� ���� ���Ѵ�.
                UpgradeTotal[type.ToString()] += UpgradeStaticManager.instance.GetVal(idx);
                idx++;
            }                
        }  

        return UpgradeTotal[type.ToString()];
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

    public void NewUpgrade(string id)
    {
        // ���Ἲ üũ
        if (true)
        {

        }  

        UpgradeActiveDict.Add(id, true);
    }


    public void SaveData()
    {
        var data =  JsonConvert.SerializeObject(UpgradeActiveDict);
        
        File.WriteAllText(Application.dataPath + _saveFileName, data);
    }
    public void LoadData()
    {
        var fileData = File.ReadAllText(Application.dataPath + _saveFileName);
        var data = JsonConvert.DeserializeObject<Dictionary<string, bool>>(fileData);
        /*
        ??
        */
        UpgradeActiveDict = data;
    }

    public void _ViewData()
    {
        var data = JsonConvert.SerializeObject(UpgradeActiveDict);
        Debug.Log(data);
    }


    private void Start()
    {

        NewUpgrade("1000");
        NewUpgrade("1001");
        //LoadData();
        SaveData();
    }
}
