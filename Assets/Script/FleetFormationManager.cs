using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FleetCheckResultData 
{
    public bool isGood = false;
    public List<int> ProbFleetIdx = new List<int>();
}

[SerializeField]
public class FleetFormation
{
    // 없으면 명시적으로 -1로 변경
    public int idType = -1;
    // 개수
    public int amount = 0;    

    // 현재 사용 비용
    public int cost = 0;    
    // 최대 비용
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

        // 새로운거 들어오면 초기화
        if (id != idType)
        {
            idType = id;
        }

        // 빈곳에 넣으면 타입 세팅 명시적으로 한번더
        if (amount == 0)
            idType = id;

        int shipCost = (int)FleetManager.instance.GetShipFormationCost(idType);
        int totalCost = qty * shipCost;

        // 사이즈 초과
        if (totalCost > size)
        {   
            // 하나조차 못들어가? 그럼 파기
            if (shipCost > size)
            {
                Remove();
                return;
            }

            // 하나라도 들어가느 크기면  최대한 들어가는 만큼 넣는다.
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
        bool isGoodToGo = true;

        // 편성된 함선들의 합, 타입당갯수를 기록 <함선 타입, 합개>
        Dictionary<int, int> dict = new Dictionary<int, int>();

        //편성에 들어간 합계
        foreach (var fleet in formations.Values)
        {
            int id_type = fleet.idType;
            if (id_type == -1)
                continue;

            if (!dict.ContainsKey(id_type))
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
                data.ProbFleetIdx.Add(id_type);
                isGoodToGo = false;                
            }
        }
        // 한 개도  없으면 안된다
        int totalQty = 0;
        foreach (var qty in dict.Values)
            totalQty += qty;

        if (totalQty <= 0)
        {
            isGoodToGo = false;
        }
        
        data.isGood = isGoodToGo;

        return data;
    }

    //GetActiveFleetIdxList : 당장은 특별한 기능없음.
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

        Debug.Log("함대 편성 데이터 불러오기 성공");
    }

    public void SaveFormationData()
    {
        string ToJsonData = JsonConvert.SerializeObject(formations);
        string filePath = Application.dataPath + FormationSaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);
                
        Debug.Log("함대 편성 데이터 저장 완료");
    }

}
