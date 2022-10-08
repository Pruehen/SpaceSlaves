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



    public class BattleShipData//키값은 id. 함선에 데이터를 입력할 때 아이디를 체크함. 아이디는 fleetQty의 index값 + 10 (index = 4일 때, id = 14)
    {
        int id = 10;//함선 아이디 (종류)
        ship_Class shipClass = ship_Class.Corvette;//함선 종류(함종)
        float dmg = 10;//발당 공격력
        dmg_Type dmgType = dmg_Type.Physical;//무기 타입
        float fireDelay = 1;//공격 속도
        float maxRange = 10;//최대 사거리
        float minRange = 5;//최소 사거리
        float hp = 100;//체력
        float df = 1;//방어력
        float sd = 100;//보호막
        float speed = 10;//이동 속도
        float agility = 10;//선회 속도
    }

    public enum dmg_Type
    {
        Physical,//물리 데미지 (실탄 무기)
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


    int max_ShipType = 20;//최대 함선 종류
    int max_ShipQty = 50;//함종별 최대 수량


    private void Start()
    {
        LoadFleetData();
        CreateShipDatas();

        if (fleetDatas.Count == 0)//기존 함대 데이터가 없을 경우, id 10부터 29까지 총 20개의 데이터 생성
        {
            for (int i = 0; i < 20; i++)
            {
                FleetSaveData fleet = new FleetSaveData();
                fleet.id = i + 10;
                fleet.qty = 0;

                fleetDatas.Add(fleet);
            }

            Debug.Log("새 함대 데이터 생성");

        }


        /*for (int i = 0; i < 20; i++)
        {
            shipInfoDatas.Add(new ShipInfoData());
        }

        string ToJsonData = JsonConvert.SerializeObject(shipInfoDatas);
        string filePath = Application.dataPath + ShipInfoDatasFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("함선 json 파일 생성 완료");*/
    }



    public string FleetSaveDataFileName = "FleetSaveData.json";
    public string ShipInfoDatasFileName = "ShipDatas.json";

    List<FleetSaveData> fleetDatas = new List<FleetSaveData>();//함종별 수량 데이터. id 10 = index 0
    List<ShipInfoData> shipInfoDatas = new List<ShipInfoData>();//함종별 속성 데이터. 위 리스트와 1:1 대응    

    public int GetFleetQtyData(int id)//id의 함선의 수량을 가져옴
    {
        if (id - 10 >= fleetDatas.Count)
            return 0;
        return fleetDatas[id - 10].qty;
    }

    public bool AddFleetData(int id, int qty_)//id 함선의 수량에 qty_만큼을 더함
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

        Debug.Log("함대 데이터 불러오기 성공");
    }

    void SaveFleetData()
    {
        string ToJsonData = JsonConvert.SerializeObject(fleetDatas);
        string filePath = Application.dataPath + FleetSaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("함대 데이터 저장 완료");
    }

    void CreateShipDatas()
    {
        string filePath = Application.dataPath + ShipInfoDatasFileName;
        string FromJsonData = File.ReadAllText(filePath);
        shipInfoDatas = JsonConvert.DeserializeObject<List<ShipInfoData>>(FromJsonData);
        Debug.Log("함선 데이터 불러오기 완료");
    }

    void OnApplicationQuit()
    {
        SaveFleetData();
    }
}

public class FleetSaveData//함대 데이터
{
    public int id;
    public int qty;
}

public class ShipInfoData
{
    public int id;//함선 고유 아이디. 10부터 시작
    public int cost;//함선 생산 가격
    public float dmg;//발당 공격력, 발당 n의 기초 데미지
    public float fireDelay;//공격 속도, n초에 1회 공격
    public float maxRange;//최대 사거리
    public float minRange;//최소 사거리
    public float fitRange;//적정 사거리. 함선은 이 거리에 머무르려고 노력함
    public float hp;//체력
    public float df;//방어력
    public float sd;//보호막    
    public float defaultspeed;//기본 이동 속도
    public float agility;//선회 속도
}
