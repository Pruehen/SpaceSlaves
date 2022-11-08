using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TimeGatedObject : MonoBehaviour
{
    public string _save_key = "DEFAULT";
    public int gateTime = 60*10;
    string _saveFileName = "/data_time_gate.txt";

    long openTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (openTime == 0)
            LoadData();

        long now = DateTime.Now.Ticks;
        if (openTime > now)
        {
            Destroy(gameObject);
        }
    }

    public void CheckGate() // 게이트를 사용, 기차 티겟 같은거 이제 소모됨
    {
        long now = DateTime.Now.Ticks;
        long maxtick = new TimeSpan(0, 0, gateTime).Ticks;
        openTime = now + maxtick;

        SaveData();
    }

    void SaveData()
    {
        string data = string.Format("{0},{1}", _save_key.ToUpper(), openTime);
        File.WriteAllText(Application.dataPath + _saveFileName, data);
    }
    void LoadData()
    {
        if (!File.Exists(Application.dataPath + _saveFileName))
        {
            openTime = DateTime.Now.Ticks;
            return;
        }

        string fileData = File.ReadAllText(Application.dataPath + _saveFileName);
        var datas = fileData.Split(",");
        openTime = long.Parse(datas[1]);
    }
}
