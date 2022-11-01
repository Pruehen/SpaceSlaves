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
    // 변경하지 말것
    NONE = 0,
    SCV_SPEED_UP =1,
    SCV_AMOUNT_UP = 2,
    COLLECTOR_CAPA = 3,
    FLEET = 4,
    FLEET_CAPA = 5,
}


// 유저가 진행한 업그래이드를 기록하고 저장하고 있다
public class UpgradeManager : MonoBehaviour
{
    public static  UpgradeManager instance;

    string _saveFileName = "/data_upgrade_prog.json";

    // id 규칙 
    // 1000 단위로 카테고리 변경

    //ex. 1000부터 1999까지는 XXX의 업그레이드다.
    //3000부터 3999까지는 YYY의 업그레이드다.
    //1000~1999 사이에 유효한 업그레이드들은 연속된 인덱스를 사용해야한다.

    //ex 1020의 다음 단계 업그레이드는 1021이여야 한다.
    //ex 1030의 다음 단계 업그레이드가 1039일수는 없다. 

    [SerializeField]
    // 활성화된 업그레이드 "id / 활성화됨"  으로 구성된 딕셔너리
    Dictionary<string, bool> UpgradeActiveDict = new Dictionary<string, bool>();
    // 업그레이드 총합을 들고 있는 딕셔너리
    Dictionary<UPGRADE_TYPE, float> UpgradeTotal = new Dictionary<UPGRADE_TYPE, float>();
    // 다음 업글 레벨
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

    static public float GetTotalActiveVal(UPGRADE_TYPE type)
    {
        var inst = UpgradeManager.instance;

        if (UpgradeManager.instance == null)
        {
            Debug.Log("!!!!!!!!!!!!!!UpgradeManger : Called before Load!!!!!!!!!!!!!!!");
            return 0;
        }

        int id = (int)type * 1000;
        if (!inst.UpgradeTotal.ContainsKey(type))
        {
            inst.RefreshUpgradeTotal(type);
        }  

        return inst.UpgradeTotal[type];
    }
    // 가지고있는 업글, 가지고있는 업글 합산, 다음 업글 단계 리프레시
    public void RefreshUpgradeTotal(UPGRADE_TYPE type) 
    {
        int id = (int)type * 1000;            
        int idx = id;
        float foundVal = 0;

        // 해당 업글이 활성화 되있는지 체크
        while (UpgradeActiveDict.ContainsKey(idx.ToString()))
        {
            foundVal += UpgradeStaticManager.instance.GetVal(idx);            
            idx++;
        }

        // 토탈에 static에서 얻어온 값을 더한다.
        UpgradeTotal.Add(type, foundVal);
    }

    public bool IsUpgradeActive(int id)
    {
        return UpgradeActiveDict.ContainsKey(id.ToString());        
    }

    //check 조건
    public bool CheckUpgradeable(int id)
    {
        bool val;
        if (UpgradeActiveDict.TryGetValue(id.ToString(), out val))
        {
            return val;
        }
        return true;
    }

    // 가장 최고 업그레이드 반환
    public int GetBestUpgradeId(UPGRADE_TYPE type)
    {
        if (UpgradeMaxId.ContainsKey(type))
            return UpgradeMaxId[type];
        return (int)type * 1000;
    }

    // 업그레이드를 한다. 
    // 에디터 사용을 위해 파라미터는 int로 
    public void DoBestUpgrade(int idType)
    {
        var type = (UPGRADE_TYPE)idType;
        _DoUpgrade(GetBestUpgradeId(type));
    }

    // 사전 업그레이드 조건체크
    bool _CheckReqUpgrade(int id, out string reqError)
    {
        bool result = true;
        int req1 = UpgradeStaticManager.instance.GetReqUpgrade1(id);
        int req2 = UpgradeStaticManager.instance.GetReqUpgrade2(id);
        reqError = "req! ";

        if (req1 != 0 && !IsUpgradeActive(req1))
        {
            reqError += req1;
            result = false;
        }
        if (req2 != 0 && !IsUpgradeActive(req2))
        {
            reqError += req1;
            result = false;
        }
        return result;
    }

    void _DoUpgrade(int id, bool isLoading = false)
    {
        // 존재 여부 체크
        // 없는 업그레이드는 진행 할 수 없다.
        if (!UpgradeStaticManager.instance.IsExist(id))
        {
            Debug.Log("상위 업그레이드 없음");
            return;
        }

        // 사전 업그레이드 조건이 충족되지 않은 업그레이드는 할수없다.
        if (!_CheckReqUpgrade(id, out string reError))
        {
            Debug.Log("먼저 업그레이드 필요 : " + reError);
            return;
        }

        // 돈 체크
        int cost = UpgradeStaticManager.instance.GetCost(id);
        if (!isLoading)
        {
            // 돈없는 상태일때
            if (!CurrencyManager.instance.CheckCurrency(CURRENCY_TYPE.Debri, cost))
            {
                Debug.Log("돈없음 / " + cost + " 필요");
                return;
            }
            else // 돈이 충분한 상태일때 
            {
                
            }
        }        

        // 다음 얼글 단계 계산
        var type = (UPGRADE_TYPE)((int)id / 1000);
        if (UpgradeMaxId.ContainsKey(type))
            UpgradeMaxId[type] = Math.Max(UpgradeMaxId[type], id + 1);
        else
            UpgradeMaxId.Add(type, id + 1);

        // 총합 계산
        if (UpgradeTotal.ContainsKey(type))
            UpgradeTotal[type] += UpgradeStaticManager.instance.GetVal(id);
        else
            UpgradeTotal.Add(type, UpgradeStaticManager.instance.GetVal(id));

        // 이 업글은 활성화
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

        Debug.Log("업그레이드 데이터 저장 완료");
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

        Debug.Log("업그레이드 데이터 불러오기 완료");
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void Start()
    {
        LoadData();
    }

    public int GetFleetLevel()
    {
        int lv = GetBestUpgradeId(UPGRADE_TYPE.FLEET) % 1000;
        return lv + 1;
    }

    // 강제로 자원 추가, 테스트용
    public void TestCur(int amount = 300)
    {
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, amount);
    }
}
