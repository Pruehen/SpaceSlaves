using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpownSystem : MonoBehaviour
{
    // �Լ� Prefab, ������ �⺻���� AI�� ����ְ� ���� �Լ� ������ ������ ��ȯ�� ���� 
    public List<GameObject> shipPrf = new List<GameObject>();

    //�Դ� ���� ������ ��ġ
    Transform spawnPos;

    public float spaceBetX = 0.2f;
    public float spaceBetZ = 0.2f;

    // �Ʊ� �Լ� ��ȯ 
    public void FriendlyShipSpown(Transform center)
    {
        spawnPos = center;

        var  activeList = FleetFormationManager.instance.GetActiveFleetIdxList();

        foreach(int fleetID in activeList)
        { 
            if (fleetID >= 0)
            {
                int shipQty = FleetFormationManager.instance.GetFleetQty(fleetID);
                int shipid = FleetFormationManager.instance.GetFleetShipIdx(fleetID);

                // �����Ͱ� ����ִ°�� 
                if (shipid == -1)
                    continue;

                for (int j = 0; j < shipQty; j++)
                {
                    var go_ship = shipPrf[shipid];

                    // �ڿ������� ��ġ ���� ��
                    int adjuster = j % 2 == 0 ? -1 : 1;
                    Vector3 randomAdjValue = new Vector3(((spaceBetX * j) * adjuster), 0, 0 - fleetID + Random.Range(-spaceBetZ, spaceBetZ));

                    Transform pos = spawnPos;
                    GameObject ship = Instantiate(go_ship, pos.position + randomAdjValue, Quaternion.identity, BattleSceneManager.instance.FriendlyManager);

                    // �±� ����
                    ship.tag = "Friend";

                    // ���� ����
                    ship.GetComponent<ShipControl>().idSet(shipid);
                }
            }
        }        
    }

    //�� �Լ� ��ȯ
    public void EnemyShipSpown(Transform center, int stageNum)
    {
        spawnPos = center;

        GameObject enemyFleet = StageManager.instance.GetStageData(stageNum).enemysPrf;
        Instantiate(enemyFleet, spawnPos.position, Quaternion.identity, center);
    }
}
