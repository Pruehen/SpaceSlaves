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



    public class BattleShipData//Ű���� id. �Լ��� �����͸� �Է��� �� ���̵� üũ��. ���̵�� fleetQty�� index�� + 10 (index = 4�� ��, id = 14)
    {
        int id = 10;//�Լ� ���̵� (����)
        ship_Class shipClass = ship_Class.Corvette;//�Լ� ����(����)
        float dmg = 10;//�ߴ� ���ݷ�
        dmg_Type dmgType = dmg_Type.Physical;//���� Ÿ��
        float fireDelay = 1;//���� �ӵ�
        float maxRange = 10;//�ִ� ��Ÿ�
        float minRange = 5;//�ּ� ��Ÿ�
        float hp = 100;//ü��
        float df = 1;//����
        float sd = 100;//��ȣ��
        float speed = 10;//�̵� �ӵ�
        float agility = 10;//��ȸ �ӵ�
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


    int max_ShipType = 20;//�ִ� �Լ� ����
    int max_ShipQty = 50;//������ �ִ� ����


    private void Start()
    {
        LoadFleetData();
        CreateShipDatas();

        if (fleetDatas.Count == 0)//���� �Դ� �����Ͱ� ���� ���, id 10���� 29���� �� 20���� ������ ����
        {
            for (int i = 0; i < 20; i++)
            {
                FleetSaveData fleet = new FleetSaveData();
                fleet.id = i + 10;
                fleet.qty = 0;

                fleetDatas.Add(fleet);
            }

            Debug.Log("�� �Դ� ������ ����");

        }


        /*for (int i = 0; i < 20; i++)
        {
            shipInfoDatas.Add(new ShipInfoData());
        }

        string ToJsonData = JsonConvert.SerializeObject(shipInfoDatas);
        string filePath = Application.dataPath + ShipInfoDatasFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("�Լ� json ���� ���� �Ϸ�");*/
    }



    public string FleetSaveDataFileName = "FleetSaveData.json";
    public string ShipInfoDatasFileName = "ShipDatas.json";

    List<FleetSaveData> fleetDatas = new List<FleetSaveData>();//������ ���� ������. id 10 = index 0
    List<ShipInfoData> shipInfoDatas = new List<ShipInfoData>();//������ �Ӽ� ������. �� ����Ʈ�� 1:1 ����    

    public int GetFleetQtyData(int id)//id�� �Լ��� ������ ������
    {
        if (id - 10 >= fleetDatas.Count)
            return 0;
        return fleetDatas[id - 10].qty;
    }

    public bool AddFleetData(int id, int qty_)//id �Լ��� ������ qty_��ŭ�� ����
    {
        if (id - 10 >= fleetDatas.Count)
            return false;

        fleetDatas[id-10].qty += qty_;
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

    void CreateShipDatas()
    {
        string filePath = Application.dataPath + ShipInfoDatasFileName;
        string FromJsonData = File.ReadAllText(filePath);
        shipInfoDatas = JsonConvert.DeserializeObject<List<ShipInfoData>>(FromJsonData);
        Debug.Log("�Լ� ������ �ҷ����� �Ϸ�");
    }

    void OnApplicationQuit()
    {
        SaveFleetData();
    }
}

public class FleetSaveData//�Դ� ������
{
    public int id;
    public int qty;
}

public class ShipInfoData
{
    public int id;//�Լ� ���� ���̵�. 10���� ����
    public int cost;//�Լ� ���� ����
    public float dmg;//�ߴ� ���ݷ�, �ߴ� n�� ���� ������
    public float fireDelay;//���� �ӵ�, n�ʿ� 1ȸ ����
    public float maxRange;//�ִ� ��Ÿ�
    public float minRange;//�ּ� ��Ÿ�
    public float fitRange;//���� ��Ÿ�. �Լ��� �� �Ÿ��� �ӹ������� �����
    public float hp;//ü��
    public float df;//����
    public float sd;//��ȣ��    
    public float defaultspeed;//�⺻ �̵� �ӵ�
    public float agility;//��ȸ �ӵ�
}
