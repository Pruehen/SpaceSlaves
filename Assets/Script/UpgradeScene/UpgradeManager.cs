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
    SCV_MORE =1,
    SCV_SPEED_UP =2,
    SCV_AMOUNT_UP = 3,
}


// 유저가 진행한 업그래이드를 기록하고 저장하고 있다
public class UpgradeManager : MonoBehaviour
{
    string _saveFileName = "/upgrade_prog_data.json";

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

    Dictionary<string, int> UpgradeTotal = new Dictionary<string, int>();

    public float GetTotalActiveVal(UPGRADE_TYPE type)
    {
        int id = (int)type * 1000;
        if (!UpgradeTotal.ContainsKey(type.ToString()))
        { 
            int idx = id;
            // 해당 업글이 활성화 되있는지 체크
            while (UpgradeActiveDict.ContainsKey(idx.ToString()))
            {
                // 토탈에 static에서 얻어온 값을 더한다.
                UpgradeTotal[type.ToString()] += UpgradeStaticManager.instance.GetVal(idx);
                idx++;
            }                
        }  

        return UpgradeTotal[type.ToString()];
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

    public void NewUpgrade(string id)
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
