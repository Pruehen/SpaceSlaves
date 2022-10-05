using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UpgradeData
{
    int id;
    int cost;
    float val;
}

// static은 게임 업그레이드가 제공하는 증가량, 가격(소모 재화량)을 들고 있다.
public class UpgradeStaticManager : MonoBehaviour
{
    public static UpgradeStaticManager instance;

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
        return 0;
    }
    public int GetVal(int id)
    {
        return 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
