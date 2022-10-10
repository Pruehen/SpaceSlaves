using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using static FleetManager;

public class FleetManager : MonoBehaviour
{
    public static FleetManager instance;
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

    int max_ShipType = 20;//�ִ� �Լ� ����
    int max_ShipQty = 50;//������ �ִ� ����


    private void Start()
    {
        LoadFleetData();

        if (fleetDatas.Count == 0)//���� �Դ� �����Ͱ� ���� ���, id 0���� 19���� �� 20���� ������ ����
        {
            for (int i = 0; i < 20; i++)
            {
                FleetSaveData fleet = new FleetSaveData();
                fleet.id = i;
                fleet.qty = 0;

                fleetDatas.Add(fleet);
            }

            Debug.Log("�� �Դ� ������ ����");

        }

        ShipDataInit();
    }

    public int GetShipCost(int id_)
    {
        int cost = shipInfoDatas[id_].cost;
        return cost;
    }


    public string FleetSaveDataFileName = "FleetSaveData.json";

    List<FleetSaveData> fleetDatas = new List<FleetSaveData>();//������ ���� ������. id 0 = index 0
    List<ShipInfoData> shipInfoDatas = new List<ShipInfoData>();//������ �Ӽ� ������. �� ����Ʈ�� 1:1 ����    

    public int GetFleetQtyData(int id)//id�� �Լ��� ������ ������
    {
        if (id >= fleetDatas.Count)
            return 0;
        return fleetDatas[id].qty;
    }

    public bool AddFleetData(int id, int qty_)//id �Լ��� ������ qty_��ŭ�� ����
    {
        if (id >= fleetDatas.Count)
            return false;

        fleetDatas[id].qty += qty_;
        return true;
    }

    void LoadFleetData()
    {
        string filePath = Application.dataPath + FleetSaveDataFileName;
        string FromJsonData = File.ReadAllText(filePath);
        fleetDatas = JsonConvert.DeserializeObject<List<FleetSaveData>>(FromJsonData);

        Debug.Log("�Դ� ������ �ҷ����� ����");
    }

    void SaveFleetData()
    {
        string ToJsonData = JsonConvert.SerializeObject(fleetDatas);
        string filePath = Application.dataPath + FleetSaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("�Դ� ������ ���� �Ϸ�");
    }

    void OnApplicationQuit()
    {
        SaveFleetData();
    }

    void ShipDataInit()
    {
        for (int i = 0; i < max_ShipType; i++)//max 20
        {
            shipInfoDatas.Add(new ShipInfoData());
            shipInfoDatas[i].DataSet(i, ship_Class.Corvette, 100, 10, dmg_Type.particle, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        }
    }
}

public class FleetSaveData//�Դ� ������
{
    public int id;
    public int qty;
}

public enum dmg_Type
{
    Physical,//���� ������ (��ź ����)
    explosion,//���� ������ (���� ����)
    particle//���� ������ (������ ����)
}
public enum ship_Class
{
    Corvette,//�ʰ���
    Frigate,//ȣ����
    Destroyer,//������
    Light_Cruiser,//�������
    heavy_cruiser,//�߼�����
    Battleship//����
}

public class ShipInfoData
{
    public int id;//�Լ� ���� ���̵�. 0���� ����
    ship_Class shipClass;//�Լ� ����(����)
    public int cost;//�Լ� ���� ����
    public float dmg;//�ߴ� ���ݷ�, �ߴ� n�� ���� ������
    dmg_Type dmgType;//���� Ÿ��
    public float fireDelay;//���� �ӵ�, n�ʿ� 1ȸ ����
    public float maxRange;//�ִ� ��Ÿ�
    public float minRange;//�ּ� ��Ÿ�
    public float fitRange;//���� ��Ÿ�. �Լ��� �� �Ÿ��� �ӹ������� �����
    public float hp;//ü��
    public float df;//����
    public float sd;//��ȣ��    
    public float defaultspeed;//�⺻ �̵� �ӵ�
    public float agility;//��ȸ �ӵ�   

    public void DataSet(int id, ship_Class ship_Class, int cost, float dmg, dmg_Type dmg_Type, float fireDelay, float maxRange, float minRange, float fitRange, float hp, 
        float df, float sd, float defoultSpeed, float agility)
    {
        this.id = id;
        this.shipClass = ship_Class;
        this.cost = cost;
        this.dmg = dmg;
        this.dmgType = dmg_Type;
        this.fireDelay = fireDelay;
        this.maxRange = maxRange;
        this.minRange = minRange;
        this.fitRange = fitRange;
        this.hp = hp;
        this.df = df;
        this.sd = sd;
        this.defaultspeed = defoultSpeed;
        this.agility = agility;
    }
}
