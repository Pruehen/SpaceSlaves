using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[SerializeField]
public class FleetFormation
{
    // ������ ��������� -1�� ����
    public int idType = -1;
    public int amount = 0;    
    
    public void Add(int id, int qty, out bool isSuccess)
    {
        int size = 10;
        isSuccess = false;

        // ���ο�� ������ �ʱ�ȭ
        if (id != idType)
        {
            idType = id;
        }

        // ����� ������ Ÿ�� ���� ��������� �ѹ���
        if (amount == 0)
            idType = id;

        // ������ �ʰ�
        if (qty > size)
        {
            qty = size;
        }

        amount = qty;
        isSuccess = true;
    }
    public void Remove()
    {
        amount = 0;
        idType = -1;
    }
}

public class FleetFormationManager : MonoBehaviour
{
    [SerializeField]
    Dictionary<int, FleetFormation> formations = new Dictionary<int, FleetFormation>();
    private string FormationSaveDataFileName = "/data_fleet_formation.json";
    
    public static FleetFormationManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        LoadFormationData();
        DontDestroyOnLoad(this);
    }

    public bool SetUnit(int id, int formIdx, int qty)
    {
        if (formations.ContainsKey(formIdx))
        {
            formations[formIdx].Add(id, qty, out bool success);
            return success;
        }

        var data = new FleetFormation();        
        data.Add(id, qty, out bool suc);
        formations.Add(formIdx, data);
        return suc;
    }

    public bool RemoveUnit(int formIdx)
    {
        if (formations.ContainsKey(formIdx))
        {
            formations[formIdx].Remove();
            return true;
        }
        return false;
    }

    public int GetFleetQty(int formIdx)
    {
        if (!formations.ContainsKey(formIdx))
            return 0;
        
        return formations[formIdx].amount;
    }

    public int GetFleetShipIdx(int formIdx)
    {
        if (!formations.ContainsKey(formIdx))
            return -1;

        return formations[formIdx].idType;
    }

    public void Test()
    {
        if (CheckeValidateData())
        {
            Debug.Log("it is good");
        }
    }

    public bool CheckeValidateData()
    {
        bool isGoodToGo = true;

        // ���� �Լ����� ��, Ÿ�Դ簹���� ��� <�Լ� Ÿ��, �հ�>
        Dictionary<int, int> dict = new Dictionary<int, int>();

        //���� �� �հ�
        foreach (var fleet in formations.Values)
        {
            int id_type = fleet.idType;
            if (id_type == -1)
                continue;

            if (dict.ContainsKey(id_type))
                dict.Add(id_type, fleet.amount);
            else
                dict[id_type] += fleet.amount;

        }
        foreach (var id_type in dict.Keys)
        {
            int curQty = dict[id_type];
            int stockQty = FleetManager.instance.GetFleetQtyData(id_type);
            if (curQty > stockQty)
            {
                isGoodToGo = false;
                break;
            }
        }
        // �� ����  ������ �ȵȴ�
        int totalQty = 0;
        foreach (var qty in dict.Values)
            totalQty += qty;

        if (totalQty <= 0)
        {
            isGoodToGo = false;
        }

        return isGoodToGo;
    }

    //GetActiveFleetIdxList : ������ Ư���� ��ɾ���.
    public List<int> GetActiveFleetIdxList()
    {
        List<int> list = new List<int>();
        foreach (var item in formations.Keys)
        {
            list.Add(item);
        }

        return list;
    }

    void OnApplicationQuit()
    {
        SaveFormationData();
    }

    void LoadFormationData()
    {
        string filePath = Application.dataPath + FormationSaveDataFileName;
        string FromJsonData = File.ReadAllText(filePath);
        formations = JsonConvert.DeserializeObject<Dictionary<int, FleetFormation>>(FromJsonData);

        Debug.Log("�Դ� �� ������ �ҷ����� ����");
    }

    public void SaveFormationData()
    {
        string ToJsonData = JsonConvert.SerializeObject(formations);
        string filePath = Application.dataPath + FormationSaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);
                
        Debug.Log("�Դ� �� ������ ���� �Ϸ�");
    }

}
