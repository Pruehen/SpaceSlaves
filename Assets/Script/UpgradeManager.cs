using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public enum UPGRADE_TYPE 
{
    SCV_MORE =1,
    SCV_SPEED_UP =2,
    SCV_AMOUNT_UP = 3,
}


public class UpgradeManager : MonoBehaviour
{
    public UpgradeManager instance;

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

    public float GetTotalActiveVal(UPGRADE_TYPE type)
    {
        float val = 0;
        return val;
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
        UpgradeActiveDict.Add(id, true);
    }

    public void LoadData()
    { 
    }
    public void SaveData()
    {
    }

    private void Start()
    {
        
    }
}
