using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

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

    public ShipInfoData GetShipData(int id_)
    {
        return shipInfoDatas[id_];
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
        }

        shipInfoDatas[0].DataSet(0, "H", ship_Class.Corvette, 100, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[1].DataSet(1, "I", ship_Class.Corvette, 200, 40, dmg_Type.kinetic, true, 5, 30, 10, 20, 150, 1, 100, 20, 20);
        shipInfoDatas[2].DataSet(2, "J", ship_Class.Corvette, 400, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[3].DataSet(3, "K", ship_Class.Frigate, 800, 10, dmg_Type.particle,false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[4].DataSet(4, "L", ship_Class.Frigate, 1600, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[5].DataSet(5, "M", ship_Class.Frigate, 3200, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[6].DataSet(6, "N", ship_Class.Destroyer, 6400, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[7].DataSet(7, "O", ship_Class.Destroyer, 12800, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[8].DataSet(8, "P", ship_Class.Destroyer, 25600, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[9].DataSet(9, "Q", ship_Class.Destroyer, 51200, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[10].DataSet(10, "R", ship_Class.Light_Cruiser, 102400, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[11].DataSet(11, "S", ship_Class.Light_Cruiser, 204800, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[12].DataSet(12, "T", ship_Class.Light_Cruiser, 409600, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[13].DataSet(13, "U", ship_Class.Light_Cruiser, 819200, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[14].DataSet(14, "V", ship_Class.heavy_cruiser, 1638400, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[15].DataSet(15, "W", ship_Class.heavy_cruiser, 3276800, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[16].DataSet(16, "X", ship_Class.heavy_cruiser, 6553600, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[17].DataSet(17, "Y", ship_Class.Battleship, 13107200, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[18].DataSet(18, "Z", ship_Class.Battleship, 26214400, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
        shipInfoDatas[19].DataSet(19, "A", ship_Class.Battleship, 52428800, 10, dmg_Type.particle, false, 1, 10, 5, 7, 100, 1, 100, 30, 20);
    }
}

public class FleetSaveData//�Դ� ������
{
    public int id;
    public int qty;
}

public enum dmg_Type
{
    kinetic,//���� ������ (��ź ����)
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
    public string shipName;//�Լ� �̸� (�Ա�)
    public ship_Class shipClass;//�Լ� ���� (����)
    public int cost;//�Լ� ���� ����
    public float dmg;//�ߴ� ���ݷ�, �ߴ� n�� ���� ������
    public dmg_Type dmgType;//���� Ÿ��
    public bool turretType;
    public float fireDelay;//���� �ӵ�, n�ʿ� 1ȸ ����
    public float maxRange;//�ִ� ��Ÿ�
    public float minRange;//�ּ� ��Ÿ�
    public float fitRange;//���� ��Ÿ�. �Լ��� �� �Ÿ��� �ӹ������� �����
    public float hp;//ü��
    public float df;//����
    public float sd;//��ȣ��    
    public float defaultspeed;//�⺻ �̵� �ӵ�
    public float agility;//��ȸ �ӵ�   

    public void DataSet(int id, string shipName, ship_Class ship_Class, int cost, float dmg, dmg_Type dmg_Type, bool turretType, float fireDelay, float maxRange, float minRange, float fitRange, float hp, 
        float df, float sd, float defoultSpeed, float agility)
    {
        this.id = id;
        this.shipName = shipName;
        this.shipClass = ship_Class;
        this.cost = cost;
        this.dmg = dmg;
        this.dmgType = dmg_Type;
        this.turretType = turretType;
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
