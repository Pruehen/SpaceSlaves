using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
    public class BattleShipData
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
        Corvette,
        Frigate,
        Destroyer,
        Light_Cruiser,
        heavy_cruiser,
        Battleship
    }

    public List<BattleShipData> shipList = new List<BattleShipData>();
}
