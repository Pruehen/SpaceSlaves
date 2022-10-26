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
        "�������� 1",
        "�������� 2",
        "�������� 3",
        "�������� 4",
        "�������� 5",
        "�������� 6",
        "�������� 7",
        "�������� 8",
        "�������� 9",
        "�������� 10",
        "�������� 11",
        "�������� 12",
        "�������� 13",
        "�������� 14",
        "�������� 15",
        "�������� 16",
        "�������� 17",
        "�������� 18",
        "�������� 19",
        "�������� 20"
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
