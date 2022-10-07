using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
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


    List<int> fleetQty = new List<int>();//함종별 수량 데이터. id 10 = index 0
    int max_ShipType = 20;//최대 함선 종류
    int max_ShipQty = 50;//함종별 최대 수량

    public bool AddShip(int id, int count)
    {       
        if(fleetQty[id - 10] >= max_ShipQty)
        {
            return false;
        }
        else
        {
            fleetQty[id - 10] += count;
            return true;
        }
    }

    private void Start()
    {
        for (int i = 0; i < max_ShipType; i++)
        {
            fleetQty.Add(0);//여기서 xml이나 json 등의 외부 데이터를 참조해서, 기존에 가지고 있던 함대 데이터를 불러온 후 입력.
        }
    }
}
