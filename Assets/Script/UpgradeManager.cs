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

    public float GetTotalActiveVal(UPGRADE_TYPE type)
    {
        float val = 0;
        return val;
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
