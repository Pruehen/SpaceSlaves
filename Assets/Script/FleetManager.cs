using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
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
    Destroyer,//������
    Cruiser,//�������
    Battleship//����
}

[Serializable]
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
    public float formationCost; // �Դ� �� ���
}

public class FleetManager : MonoBehaviour
{
    string _saveFileName = "/static_ship_data.txt";

    public static FleetManager instance;

    public Sprite[] shipImages;

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
        LoadStaticData();
        LoadFleetData();

        if (fleetDatas.Count == 0)//���� �Դ� �����Ͱ� ���� ���, id 0���� 11���� �� 20���� ������ ����
        {
            for (int i = 0; i < 12; i++)
            {
                FleetSaveData fleet = new FleetSaveData();
                fleet.id = i;
                fleet.qty = 0;

                fleetDatas.Add(fleet);
            }

            Debug.Log("�� �Դ� ������ ����");
        }
    }

    public ShipInfoData GetShipData(int id_)
    {
        return staticShipDatas[id_];
    }

    public string GetShipName(int id_)
    {
        return GetShipData(id_).shipName;
    }

    public float GetShipFormationCost(int id_)
    {
        return GetShipData(id_).formationCost;
    }

    public Sprite GetShipImage(int id_)
    {
        return shipImages[Math.Clamp(id_, 0, shipImages.Length-1)];
    }

    string FleetSaveDataFileName = "/data_fleet_save.txt";

    List<FleetSaveData> fleetDatas = new List<FleetSaveData>();//������ ���� ������. id 0 = index 0
    List<ShipInfoData> staticShipDatas = new List<ShipInfoData>();//������ �Ӽ� ������. �� ����Ʈ�� 1:1 ����    

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
        SaveFleetData();
        return true;
    }

    public void DecreaseFleetData(int id, int _qty) //id �Լ��� ������ qty_��ŭ�� ����
    {
        fleetDatas[id].qty -= _qty;
        fleetDatas[id].qty = Mathf.Max(0, fleetDatas[id].qty);
        
    }

    void LoadFleetData()
    {
        if (!File.Exists(Application.dataPath + FleetSaveDataFileName))
            return;
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
        
    void LoadStaticData()
    {
        if (staticShipDatas.Count == 0)
        {
            if (!File.Exists(Application.dataPath + _saveFileName))
                return;

            var fileData = File.ReadAllText(Application.dataPath + _saveFileName);
            var data = JsonConvert.DeserializeObject<List<ShipInfoData>>(fileData);

            staticShipDatas = data;
        }

    }


}


