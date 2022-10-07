using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public enum UPGRADE_TYPE 
{
    // 변경하지 말것
    SCV_SPEED_UP =1,
    SCV_MORE =2,
    SCV_AMOUNT_UP = 3,
}


// 유저가 진행한 업그래이드를 기록하고 저장하고 있다
public class UpgradeManager : MonoBehaviour
{
    string _saveFileName = "/data_upgrade_prog.json";

    public static  UpgradeManager instance;

    // id 규칙 
    // 1000 단위로 카테고리 변경

    //ex. 1000부터 1999까지는 XXX의 업그레이드다.
    //3000부터 3999까지는 YYY의 업그레이드다.
    //1000~1999 사이에 유효한 업그레이드들은 연속된 인덱스를 사용해야한다.

    //ex 1020의 다음 단계 업그레이드는 1021이여야 한다.
    //ex 1030의 다음 단계 업그레이드가 1039일수는 없다. 

    [SerializeField]
    // "id / 활성화됨"  으로 구성된 딕셔너리
    Dictionary<string, bool> UpgradeActiveDict = new Dictionary<string, bool>();
    // 업그레이드 총합을 들고 있는 딕셔너리
    Dictionary<string, float> UpgradeTotal = new Dictionary<string, float>();

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
            int idx = id;
            float foundVal = 0;
            // 해당 업글이 활성화 되있는지 체크
            while (UpgradeActiveDict.ContainsKey(idx.ToString()))
            {
                // 활성화된 업글이 올려주는 값을 가져와 합산.
                foundVal += UpgradeStaticManager.instance.GetVal(idx);
                idx++;
            }
            if (foundVal <= 0)
                return 0;

            // 토탈에 static에서 얻어온 값을 더한다.
            UpgradeTotal.Add(type.ToString(), foundVal);
        }  

        return UpgradeTotal[type.ToString()];
    }

    public void RefreshUpgradeTotal(UPGRADE_TYPE type) 
    {
        int id = (int)type * 1000;            
        int idx = id;
        // 해당 업글이 활성화 되있는지 체크
        while (UpgradeActiveDict.ContainsKey(idx.ToString()))
        {
            float foundVal = UpgradeStaticManager.instance.GetVal(idx);
            // 토탈에 static에서 얻어온 값을 더한다.
            UpgradeTotal[type.ToString()] += foundVal;
            idx++;
        }
    }

    //check 조건
    public bool CheckUpgradeable(string id)
    {
        bool val;
        if (UpgradeActiveDict.TryGetValue(id, out val))
        {
            return val;
        }
        return true;
    }

    // 업그레이드를 한다.
    public void DoUpgrade(string id)
    {
        // 무결성 체크
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
        if (!File.Exists(Application.dataPath + _saveFileName))
            return;

        var fileData = File.ReadAllText(Application.dataPath + _saveFileName);
        var data = JsonConvert.DeserializeObject<Dictionary<string, bool>>(fileData);
        UpgradeActiveDict = data;
    }

    // 전체 업글된 항목을 출력, 테스트용임 
    public void _ViewData()
    {
        var data = JsonConvert.SerializeObject(UpgradeActiveDict);
        Debug.Log(data);
    }


    private void Start()
    {
        LoadData();        
        DoUpgrade("1000");
        DoUpgrade("1001");
        DoUpgrade("1002");
        DoUpgrade("1003");
        DoUpgrade("1004");
        DoUpgrade("1005");
        DoUpgrade("1006");
        DoUpgrade("1007");
    }
}
