using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpownSystem : MonoBehaviour
{
    // 함선 Prefab, 외형과 기본적인 AI만 담고있고 세부 함선 깡스펙 세팅은 소환시 해줌 
    public List<GameObject> shipPrf = new List<GameObject>();

    //함대 마다 스폰될 위치
    Transform spawnPos;

    public float spaceBetX = 0.2f;
    public float spaceBetZ = 0.2f;

    // 아군 함선 소환 
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

                // 데이터가 비어있는경우 
                if (shipid == -1)
                    continue;

                for (int j = 0; j < shipQty; j++)
                {
                    var go_ship = shipPrf[shipid];

                    // 자연스러운 위치 조정 값
                    int adjuster = j % 2 == 0 ? -1 : 1;
                    Vector3 randomAdjValue = new Vector3(((spaceBetX * j) * adjuster), 0, 0 - fleetID + Random.Range(-spaceBetZ, spaceBetZ));

                    Transform pos = spawnPos;
                    GameObject ship = Instantiate(go_ship, pos.position + randomAdjValue, Quaternion.identity, BattleSceneManager.instance.FriendlyManager);

                    // 태그 세팅
                    ship.tag = "Friend";

                    // 스팩 세팅
                    ship.GetComponent<ShipControl>().idSet(shipid);
                }
            }
        }        
    }

    //적 함선 소환
    public void EnemyShipSpown(Transform center, int stageNum)
    {
        spawnPos = center;

        GameObject enemyFleet = StageManager.instance.GetStageData(stageNum).enemysPrf;
        Instantiate(enemyFleet, spawnPos.position, Quaternion.identity, center);
    }
}
