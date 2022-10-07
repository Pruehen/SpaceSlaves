using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
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


    List<int> fleetQty = new List<int>();//������ ���� ������. id 10 = index 0
    int max_ShipType = 20;//�ִ� �Լ� ����
    int max_ShipQty = 50;//������ �ִ� ����

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
            fleetQty.Add(0);//���⼭ xml�̳� json ���� �ܺ� �����͸� �����ؼ�, ������ ������ �ִ� �Դ� �����͸� �ҷ��� �� �Է�.
        }
    }
}
