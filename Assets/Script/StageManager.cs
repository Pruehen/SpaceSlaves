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

    public static int STAGE_COUNT = 10;
    List<StageData> stages = new List<StageData>();//스테이지 클래스들을 담고 있는 리스트

    public StageData GetStageData(int index)//변수로 인덱스를 넣으면 인덱스에 맞는 스테이지를 리턴해줌 (0 = 0스테이지 (표기는 1스테이지))
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
        "스테이지 10"
    };

    string[] stageInfoArray =
{
        "스테이지 1 에 대한 설명을 적으시오",
        "한때는 매섭게 타오르던 행성이었지만 \n적들의 자원채굴을 위해 내부가 무참히 파여진 상태이다. ",
        "태양이 모든걸 태워버릴듯한 행성에서 적들의 식량이 재배되고 있다. ",
        "적들의 주요 거주지가 위치한 행성, \n우리에게 준 절망을 되갚아줄 차례다. ",
        "",
        "",
        "",
        "",
        "",
        ""
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
