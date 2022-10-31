using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



public class ShipAi : MonoBehaviour
{
    ShipControl mainShipInfo;

    public enum State
    {
        run,
        back,
        idle
    }
    State state = State.idle;

    private void Awake()
    {
        
    }

    void Start()
    {
        mainShipInfo = this.gameObject.GetComponent<ShipControl>();
        this.gameObject.layer = this.transform.parent.gameObject.layer;

        if (this.gameObject.layer == 8)//friedlyLayer
        {
            enemyManager = BattleSceneManager.instance.FriendlyManager;
        }
        else if (this.gameObject.layer == 7)//enemyLayer
        {
            enemyManager = BattleSceneManager.instance.EnemyManager.GetChild(0);
        }

        Invoke("TargetFound", 1);
        Invoke("TargetSet", 0.2f);
    }

    private void FixedUpdate()
    {
        AiStateSet();

        // 상태에 따른 이동 처리
        if (state == State.run)
        {
            mainShipInfo.MoveFor();
        }
        else if (state == State.back)
        {
            mainShipInfo.MoveBack();
        }
        else if (state == State.idle)
        {

        }
    }

    public void AiStateSet()
    {
        if (mainShipInfo.toTargetVec.magnitude * 10 >= mainShipInfo.maxRange)
        {
            state = State.run;
        }
        else if (mainShipInfo.toTargetVec.magnitude * 10 < mainShipInfo.maxRange && mainShipInfo.toTargetVec.magnitude * 10 > mainShipInfo.minRange)
        {
            state = State.idle;
        }
        else if (mainShipInfo.toTargetVec.magnitude * 10 <= mainShipInfo.minRange)
        {
            state = State.back;
        }
    }

    Transform enemyManager;//적 트랜스폼 검색 용도
    public void TargetFound()
    {
        float ShortDis;

        ShortDis = Vector3.Distance(gameObject.transform.position, mainShipInfo.FoundTarget.First.Value.transform.position);

        mainShipInfo.target = mainShipInfo.FoundTarget.First.Value;

        foreach (GameObject found in mainShipInfo.FoundTarget)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
            
            if (Distance < ShortDis && found.activeSelf)
            {
                mainShipInfo.target = found;
                //mainShipInfo.targetNode = found;
                ShortDis = Distance;
            }
        }
    }

    void TargetSet()
    {
        for (int i = 0; i < enemyManager.childCount; i++)
        {
            mainShipInfo.FoundTarget.AddLast(enemyManager.GetChild(i).gameObject);
        }
    }
}
