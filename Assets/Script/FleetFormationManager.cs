using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FleetCheckResultData 
{
    public Dictionary<int, bool> ProbFleetIdx = new Dictionary<int, bool>();
    public bool isEmpty = false;
    public bool isFleetHaveProblem(int id)
    {
        return ProbFleetIdx.ContainsKey(id) ? ProbFleetIdx[id] : false;
    }
    public bool isGood 
    {
        get 
        {            
            foreach (var item in ProbFleetIdx.Values)
            {
                if (item) // �ϳ��� ���������� ����
                    return false;             
            }
            // ��� ������ �������� ���� ������ ���ų�, ������ �Դ밡 ��������� �������̱�
            return !isEmpty;

        }
    }
}

[SerializeField]
public class FleetFormation
{
    // �Լ� ����, ������ ��������� -1�� ����
    public int idType = -1;
    // ����
    public int amount = 0;    

    // ���� ��� ���
    public int cost = 0;    
    // �ִ� ���
    public int size 
    {
        get
        {
            return 10 + (int)UpgradeManager.GetTotalActiveVal(UPGRADE_TYPE.FLEET_CAPA);
        } 
    }
    
    public void Add(int id, int qty, out bool isSuccess)
    {
        isSuccess = false;

        // ���ο�� ������ �ʱ�ȭ
        if (id != idType)
        {
            idType = id;
        }

        // ����� ������ Ÿ�� ���� ��������� �ѹ���
        if (amount == 0)
            idType = id;

        int shipCost = (int)FleetManager.instance.GetShipFormationCost(idType);
        int totalCost = qty * shipCost;

        // ������ �ʰ�
        if (totalCost > size)
        {   
            // �ϳ����� ����? �׷� �ı�
            if (shipCost > size)
            {
                Remove();
                return;
            }

            // �ϳ��� ���� ũ���  �ִ��� ���� ��ŭ �ִ´�.
            qty = size / shipCost;
            totalCost = qty * shipCost;
        }

        amount = qty;
        cost = totalCost;
        isSuccess = true;

        Debug.Log(string.Format("{0} / {1}", amount, size ));
    }
    public void Remove()
    {
        cost = 0;
        amount = 0;
        idType = -1;
    }
}

public class FleetFormationManager : MonoBehaviour
{
    [SerializeField]
    Dictionary<int, FleetFormation> formations = new Dictionary<int, FleetFormation>();
    private string FormationSaveDataFileName = "/data_fleet_formation.txt";
    
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
        if (qty <= 0)
            return false;

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


    public int GetFleetCost(int formIdx)
    {
        if (!formations.ContainsKey(formIdx))
            return 0;

        return formations[formIdx].cost;
    }

    public int GetFleetMaxSize(int formIdx)
    {
        if (!formations.ContainsKey(formIdx))
            return 10 + (int)UpgradeManager.GetTotalActiveVal(UPGRADE_TYPE.FLEET_CAPA);

        return formations[formIdx].size;
    }

    public int GetShipQty(int shipId)
    {
        int cnt = 0;

        foreach (var item in formations.Values )
        {
            if ( item.idType == shipId)
                cnt += item.amount;
        }

        return cnt;
    }

    public int GetFleetShipIdx(int formIdx)
    {
        if (!formations.ContainsKey(formIdx))
            return -1;

        return formations[formIdx].idType;
    }

    public void Test()
    {
        if (CheckValidateData())
        {
            Debug.Log("it is good");
        }
    }

    
    public bool CheckValidateData()
    {
        FleetCheckResultData data = MakeValidateData();
        return data.isGood;
    }
    public FleetCheckResultData MakeValidateData()
    {
        FleetCheckResultData data = new FleetCheckResultData();

        // ���� �Լ����� ��, Ÿ�Դ簹���� ��� <�Լ� Ÿ��, �հ�>
        Dictionary<int, int> dict = new Dictionary<int, int>();

        //���� �� �հ�, dict�� ������
        foreach (var fleetid in formations.Keys)
        {
            int shiptype= formations[fleetid].idType;
            if (shiptype == -1 || formations[fleetid].amount <= 0)
                continue;

            if (!dict.ContainsKey(shiptype))
                dict.Add(shiptype, formations[fleetid].amount);
            else
                dict[shiptype] += formations[fleetid].amount;
        }
        //ProbFleetIdx�� ������
        foreach (var fleetid in formations.Keys)
        {
            int shiptype = formations[fleetid].idType;
            if (shiptype == -1)
            {
                continue;
            }
            int stockQty = FleetManager.instance.GetFleetQtyData(shiptype);
            int curQty = dict.ContainsKey(shiptype) ? dict[shiptype] : 0;
            data.ProbFleetIdx.Add(fleetid, curQty > stockQty);
        }
        // �� ���� ������ �ȵȴ�
        int totalQty = 0;
        foreach (var qty in dict.Values)
            totalQty += qty;

        data.isEmpty = totalQty <= 0;

        return data;
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
