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
    List<StageData> stages = new List<StageData>();//�������� Ŭ�������� ��� �ִ� ����Ʈ

    public StageData GetStageData(int index)//������ �ε����� ������ �ε����� �´� ���������� �������� (0 = 0�������� (ǥ��� 1��������))
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

            if(!StageFleetData.ContainsKey(shipName))//�Լ��� ���ʷ� Ž���Ǿ��� ���, value 1�� �ʱ�ȭ
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
        "�������� 1",
        "�������� 2",
        "�������� 3",
        "�������� 4",
        "�������� 5",
        "�������� 6",
        "�������� 7",
        "�������� 8",
        "�������� 9",
        "�������� 10"
    };

    string[] stageInfoArray =
{
        "�������� 1 �� ���� ������ �����ÿ�",
        "�Ѷ��� �ż��� Ÿ������ �༺�̾����� \n������ �ڿ�ä���� ���� ���ΰ� ������ �Ŀ��� �����̴�. ",
        "�¾��� ���� �¿��������� �༺���� ������ �ķ��� ���ǰ� �ִ�. ",
        "������ �ֿ� �������� ��ġ�� �༺, \n�츮���� �� ������ �ǰ����� ���ʴ�. ",
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
