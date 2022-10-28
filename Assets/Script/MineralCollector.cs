using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralCollector : MonoBehaviour
{


    int MAX_TIME = (60 * 60) * 2;
    int MIN_TIME = (60 * 10);

    long recentCollectedTime = 0;
    long maxRewardTime = 0;
    long minRequiredTime = 0;

    public void ClaimRewards()
    {
        var now = DateTime.Now.Ticks;
        recentCollectedTime  = now;
        minRequiredTime = recentCollectedTime + new TimeSpan(0,0, MIN_TIME).Ticks;
        maxRewardTime = recentCollectedTime + new TimeSpan(0, 0, MAX_TIME).Ticks;

        Debug.Log(string.Format("{0} , {1}, {2}s need to be elasped", 
            new TimeSpan(maxRewardTime).TotalSeconds, 
            new TimeSpan(minRequiredTime).TotalSeconds,
            new TimeSpan(recentCollectedTime - maxRewardTime).TotalSeconds
            ));
    }

    // Start is called before the first frame update
    void Start()
    {
        ClaimRewards();
    }

    private void OnDestroy()
    {
    }
}
