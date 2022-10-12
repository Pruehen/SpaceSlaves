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

    int max_ShipType = 20;//최대 함선 종류
    int max_ShipQty = 50;//함종별 최대 수량


    private void Start()
    {
        LoadFleetData();

        if (fleetDatas.Count == 0)//기존 함대 데이터가 없을 경우, id 0부터 19까지 총 20개의 데이터 생성
        {
            for (int i = 0; i < 20; i++)
            {
                FleetSaveData fleet = new FleetSaveData();
                fleet.id = i;
                fleet.qty = 0;

                fleetDatas.Add(fleet);
            }

            Debug.Log("새 함대 데이터 생성");

        }

        ShipDataInit();
    }

    public ShipInfoData GetShipData(int id_)
    {
        return shipInfoDatas[id_];
    }


    public string FleetSaveDataFileName = "FleetSaveData.json";

    List<FleetSaveData> fleetDatas = new List<FleetSaveData>();//함종별 수량 데이터. id 0 = index 0
    List<ShipInfoData> shipInfoDatas = new List<ShipInfoData>();//함종별 속성 데이터. 위 리스트와 1:1 대응    

    public int GetFleetQtyData(int id)//id의 함선의 수량을 가져옴
    {
        if (id >= fleetDatas.Count)
            return 0;
        return fleetDatas[id].qty;
    }

    public bool AddFleetData(int id, int qty_)//id 함선의 수량에 qty_만큼을 더함
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

        Debug.Log("함대 데이터 불러오기 성공");
    }

    void SaveFleetData()
    {
        string ToJsonData = JsonConvert.SerializeObject(fleetDatas);
        string filePath = Application.dataPath + FleetSaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("함대 데이터 저장 완료");
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

public class FleetSaveData//함대 데이터
{
    public int id;
    public int qty;
}

public enum dmg_Type
{
    kinetic,//물리 데미지 (실탄 무기)
    explosion,//폭발 데미지 (폭발 무기)
    particle//입자 데미지 (레이저 무기)
}
public enum ship_Class
{
    Corvette,//초계함
    Frigate,//호위함
    Destroyer,//구축함
    Light_Cruiser,//경순양함
    heavy_cruiser,//중순양함
    Battleship//전함
}

public class ShipInfoData
{
    public int id;//함선 고유 아이디. 0부터 시작
    public string shipName;//함선 이름 (함급)
    public ship_Class shipClass;//함선 종류 (함종)
    public int cost;//함선 생산 가격
    public float dmg;//발당 공격력, 발당 n의 기초 데미지
    public dmg_Type dmgType;//무기 타입
    public bool turretType;
    public float fireDelay;//공격 속도, n초에 1회 공격
    public float maxRange;//최대 사거리
    public float minRange;//최소 사거리
    public float fitRange;//적정 사거리. 함선은 이 거리에 머무르려고 노력함
    public float hp;//체력
    public float df;//방어력
    public float sd;//보호막    
    public float defaultspeed;//기본 이동 속도
    public float agility;//선회 속도   

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
