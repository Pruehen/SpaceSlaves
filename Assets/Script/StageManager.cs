using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static CurrencyManager;

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
    List<bool> stageClearData = new List<bool>();

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
        "먼저 저들의 정찰을 뚫어야 한다. ",
        "한때는 매섭게 타오르던 행성이었지만 \n적들의 자원 채굴을 위해 내부가 무참히 파여진 상태이다. ",
        "태양이 모든 걸 태워버릴 듯한 행성에서 적들의 식량이 재배되고 있다. ",
        "적들의 주요 거주지가 위치한 행성, \n우리에게 준 절망을 갚아줄 차례다. ",
        "거대한 연료탱크로 전락해버리고 만 행성에 자유를 되찾아줄 시간이다. ",
        "일부 적 노동자들이 자원을 채취하고 있다. \n저들에게서 항복의 의사가 들려오는 것 같은데 정말일까? ",
        "적의 예전 사령부인 만큼 이만한 요충지가 없다. 반드시 탈환하자. ",
        "마침내 적의 사령부에 도달하였다. 전력으로 부딪혀야 한다. ",
        "잠시 재정비를 위해 온 곳에서 여유롭게 휴양을 즐기는 적 고위 간부를 마주쳤다. \n이런!",
        "남은 잔당들이 지구를 포위했다. 최후의 전투를 준비하자. "
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

        if (LoadStageData())//데이터 로딩 시도
        {
            //데이터 로딩 성공시
            for (int i = 0; i < STAGE_COUNT; i++)
            {
                stages[i].isClear = stageClearData[i];//저장된 클리어 여부 데이터를 새로 생성된 스테이지 데이터에 입력
            }
        }
        else
        {
            for (int i = 0; i < STAGE_COUNT; i++)//데이터 로딩 실패시
            {
                stageClearData[i] = false;//새로 초기화
            }
        }
        SaveStageData();

    }

    string StageSaveDataFileName = "/data_stage_save.txt";

    bool LoadStageData()
    {
        if (!File.Exists(Application.dataPath + StageSaveDataFileName))
            return false;
        string filePath = Application.dataPath + StageSaveDataFileName;
        string FromJsonData = File.ReadAllText(filePath);
        stageClearData = JsonConvert.DeserializeObject<List<bool>>(FromJsonData);

        Debug.Log("스테이지 데이터 불러오기 성공");
        return true;
    }

    public void SaveStageData()
    {
        for (int i = 0; i < STAGE_COUNT; i++)
        {
            stageClearData[i] = stages[i].isClear;//스테이지 데이터의 클리어 여부 데이터를 저장할 데이터에 입력
        }

        string ToJsonData = JsonConvert.SerializeObject(stageClearData);
        string filePath = Application.dataPath + StageSaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("스테이지 데이터 저장 완료");
    }

    void OnApplicationQuit()
    {
        SaveStageData();
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


