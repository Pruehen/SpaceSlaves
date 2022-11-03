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

    public Dictionary<string, int> GetStageFleetData(int stageNum)
    {
        Dictionary<string, int> StageFleetData = new Dictionary<string, int>();
        GameObject item = stageEnemyPrfs[stageNum];

        List<int> ints = new List<int>();

        for(int i = 0; i < item.transform.childCount; i++)
        {
            string shipName = (item.transform.GetChild(i).gameObject.name).Split('_')[0];

            if(!StageFleetData.ContainsKey(shipName))//함선이 최초로 탐색되었을 경우, value 1로 초기화
            {
                StageFleetData.Add(shipName, 1);
            }
            else
            {
                StageFleetData[shipName]++;
            }
        }

        return StageFleetData;
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

    string[] stageInfoArray =
{
        "스테이지 1 에 대한 설명을 적으시오",
        "스테이지 2 에 대한 설명을 적으시오",
        "스테이지 3 에 대한 설명을 적으시오",
        "스테이지 4 에 대한 설명을 적으시오",
        "스테이지 5 에 대한 설명을 적으시오",
        "스테이지 6 에 대한 설명을 적으시오",
        "스테이지 7 에 대한 설명을 적으시오",
        "스테이지 8 에 대한 설명을 적으시오",
        "스테이지 9 에 대한 설명을 적으시오",
        "스테이지 10 에 대한 설명을 적으시오",
        "스테이지 11 에 대한 설명을 적으시오",
        "스테이지 12 에 대한 설명을 적으시오",
        "스테이지 13 에 대한 설명을 적으시오",
        "스테이지 14 에 대한 설명을 적으시오",
        "스테이지 15 에 대한 설명을 적으시오",
        "스테이지 16 에 대한 설명을 적으시오",
        "스테이지 17 에 대한 설명을 적으시오",
        "스테이지 18 에 대한 설명을 적으시오",
        "스테이지 19 에 대한 설명을 적으시오",
        "스테이지 20 에 대한 설명을 적으시오"
    };

    public string GetStageStoryString(int stageNum)
    {
        return stageInfoArray[stageNum];
    }

    public GameObject[] stageEnemyPrfs = new GameObject[STAGE_COUNT];

    public int selectedStage = 0;

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
