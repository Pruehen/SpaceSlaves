using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

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

    public static int STAGE_COUNT = 20;
    List<StageData> stages = new List<StageData>();

    public StageData GetStageData(int index)
    {
        return stages[index];
    }

    string[] stageNameArray =
    {
        "스테이지 1",
        "스테이지 2",
        "스테이지 3",
        "스테이지 4",
        "스테이지 5",
        "스테이지 6",
        "스테이지 7",
        "스테이지 8",
        "스테이지 9",
        "스테이지 10",
        "스테이지 11",
        "스테이지 12",
        "스테이지 13",
        "스테이지 14",
        "스테이지 15",
        "스테이지 16",
        "스테이지 17",
        "스테이지 18",
        "스테이지 19",
        "스테이지 20"
    };

    public GameObject[] stageEnemyPrfs = new GameObject[STAGE_COUNT];


    private void Start()
    {
        for (int i = 0; i < STAGE_COUNT; i++)
        {
            StageData data = new StageData();
            data.SetStageData(i, stageNameArray[i], stageEnemyPrfs[i]);

            stages.Add(data);
        }
    }
}

public class StageData
{
    public int stageNum;
    public string stageName;
    public GameObject enemysPrf;
    public bool isClear = false;

    public void SetStageData(int num, string name, GameObject enemysPrf)
    {
        stageNum = num;
        stageName = name;
        this.enemysPrf = enemysPrf;
    }
}
