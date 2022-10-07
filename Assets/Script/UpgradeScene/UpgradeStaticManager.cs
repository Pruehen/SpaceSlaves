using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[SerializeField]
public struct UpgradeData
{
    public int cost;
    public float addval;
}

// static은 게임 업그레이드가 제공하는 증가량, 가격(소모 재화량)을 들고 있다.
public class UpgradeStaticManager : MonoBehaviour
{

    string _saveFileName = "/static_data_upgrade.json";
    public static UpgradeStaticManager instance;

    Dictionary<string, UpgradeData> upgradeStaticData = new Dictionary<string, UpgradeData>();

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

    public int GetCost(int id)
    {
        
        return upgradeStaticData[id.ToString()].cost;
    }
    public float GetVal(int id)
    {
        return upgradeStaticData[id.ToString()].addval;
    }

    public void LoadData()
    {
        if (!File.Exists(Application.dataPath + _saveFileName))
            return;

        var fileData = File.ReadAllText(Application.dataPath + _saveFileName);
        var data = JsonConvert.DeserializeObject<Dictionary<string, UpgradeData>>(fileData);
        upgradeStaticData = data;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }
}
