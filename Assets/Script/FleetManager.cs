using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
    public class BattleShipData
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
        Corvette,
        Frigate,
        Destroyer,
        Light_Cruiser,
        heavy_cruiser,
        Battleship
    }

    public List<BattleShipData> shipList = new List<BattleShipData>();
}
