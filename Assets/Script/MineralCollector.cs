using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MineralCollector : MonoBehaviour
{
    public TimerUI goRemainTime;
    public TMPro.TMP_Text goTextExpectedReward;

    int MAX_TIME = (60 * 60) * 10; // sec
    int MIN_TIME = (60 * 10);      // sec
    int MAX_REWARD = 10000;


    long recentCollectedTime = 0;
    long maxRewardTime = 0;
    long minRequiredTime = 0;

    string _saveFileName = "/data_m_collector.txt";

    // 
    bool test_PopEffect = false;

    public void DoPopEffect()
    {
        var poper = GetComponent<PopEffects>();
        poper.Pop(50);
    }

    public GameObject Smessage;

    public void ClaimRewards()
    {
        var now = DateTime.Now.Ticks;
        // 최소 필요 시간이 지나야 함
        if (minRequiredTime > now)
        {
            // 조건 불충분에서 버튼을 누를시 테스트 시작
            if (test_PopEffect)
            {
                DoPopEffect();
            }

            Debug.Log("최소 10분은 기다려야 한다." + " " + new TimeSpan(minRequiredTime - now).Minutes + "분 남음");
            Smessage.SendMessage("MessageQFade", "최소 10분은 기다려야 한다." + " " + new TimeSpan(minRequiredTime - now).Minutes + "분 남음");
            return;
        }

        DoPopEffect();

        int maxReward = MAX_REWARD + (int)UpgradeManager.GetTotalActiveVal(UPGRADE_TYPE.COLLECTOR_CAPA);

        var rewardValTick = Math.Min(now, maxRewardTime) - recentCollectedTime;

        long mintick = new TimeSpan(0, 0, MIN_TIME).Ticks;
        long maxtick = new TimeSpan(0, 0, MAX_TIME).Ticks;

        recentCollectedTime = now;
        minRequiredTime = recentCollectedTime + mintick;
        maxRewardTime = recentCollectedTime + maxtick;


        var reward = maxReward * (float)(TimeSpan.FromTicks(rewardValTick).TotalSeconds / TimeSpan.FromTicks(maxtick).TotalSeconds);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, (int)reward);

        Debug.Log("Rewarded : " + reward);

        Debug.Log(string.Format("{0} , {1}, {2}s need to be elasped",
            new TimeSpan(maxRewardTime).TotalSeconds,
            new TimeSpan(minRequiredTime).TotalSeconds,
            new TimeSpan(recentCollectedTime - maxRewardTime).TotalSeconds
            ));

        SaveData();

        RefreshReward();
        RefreshTimer();
    }

    public void SaveData()
    {
        string data = string.Format("{0},{1},{2}", recentCollectedTime, maxRewardTime, minRequiredTime);
        File.WriteAllText(Application.dataPath + _saveFileName, data);
        Debug.Log("광물 수집기 가동기록 저장됨");
    }
    public void LoadData()
    {
        if (!File.Exists(Application.dataPath + _saveFileName))
            return;
        string fileData = File.ReadAllText(Application.dataPath + _saveFileName);

        var datas = fileData.Split(",");

        recentCollectedTime = long.Parse(datas[0]);
        maxRewardTime = long.Parse(datas[1]);
        minRequiredTime = long.Parse(datas[2]);

        Debug.Log("광물 수집기 가동기록 불러옴");
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        if (recentCollectedTime == 0)
        {

            var now = DateTime.Now.Ticks;
            recentCollectedTime = now;

            long mintick = new TimeSpan(0, 0, MIN_TIME).Ticks;
            long maxtick = new TimeSpan(0, 0, MAX_TIME).Ticks;
            minRequiredTime = recentCollectedTime + mintick;
            maxRewardTime = recentCollectedTime + maxtick;
        }

        InvokeRepeating("RefreshTimer", 0, 1);
        InvokeRepeating("RefreshReward", 0, 1);
    }
    
    void RefreshReward()
    {
        var now = DateTime.Now.Ticks;
        long maxtick = new TimeSpan(0, 0, MAX_TIME).Ticks;
        var rewardValTick = Math.Min(now, maxRewardTime) - recentCollectedTime;

        var reward = MAX_REWARD * (float)(TimeSpan.FromTicks(rewardValTick).TotalSeconds / TimeSpan.FromTicks(maxtick).TotalSeconds);
        string rewardText;
        if (reward >= 10000)
            rewardText = (reward / 1000).ToString() + "K";
        else
            rewardText = ((int)reward).ToString();

        goTextExpectedReward.text = rewardText;
    }
    void RefreshTimer()
    {
        if (goRemainTime == null)
            return;
        var now = DateTime.Now.Ticks;
        long maxtick = new TimeSpan(0, 0, MAX_TIME).Ticks;
        goRemainTime.SetAvail(now >= minRequiredTime);
        goRemainTime.SetMax(now >= maxRewardTime);
        goRemainTime.UpdateTimer( Math.Min(now - recentCollectedTime, maxtick));
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
}
