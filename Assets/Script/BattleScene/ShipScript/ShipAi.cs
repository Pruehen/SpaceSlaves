using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum State
{
    run,
    Back,
    Idle
}

public class ShipAi : MonoBehaviour
{
    ShipControl mainShipInfo;


    State state = State.run;

    void Start()
    {
        mainShipInfo = this.gameObject.GetComponent<ShipControl>();
        state = State.run;

        InvokeRepeating("TargetFound", 0, 1);

        TargetSet();
    }

    private void Update()
    {
        PlayerAi();
    }

    public void PlayerAi()
    {
        if (mainShipInfo.toTargetVec.magnitude * 10 >= mainShipInfo.fitRange)
        {
            state = State.run;
        }
        else if (mainShipInfo.toTargetVec.magnitude * 10 < mainShipInfo.fitRange && mainShipInfo.toTargetVec.magnitude * 10 > mainShipInfo.minRange)
        {
            state = State.Idle;
        }
        else if (mainShipInfo.toTargetVec.magnitude * 10 <= mainShipInfo.minRange)
        {
            state = State.Back;
        }

        mainShipInfo.StateControl(state);
    }

    public Transform enemyManager;//적 트랜스폼 검색 용도
    void TargetFound()
    {
        float ShortDis;

        ShortDis = Vector3.Distance(gameObject.transform.position, mainShipInfo.FoundTarget[0].transform.position);

        mainShipInfo.target = mainShipInfo.FoundTarget[0];

        foreach (GameObject found in mainShipInfo.FoundTarget)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < ShortDis)
            {
                mainShipInfo.target = found;
                ShortDis = Distance;
            }
        }
    }

    void TargetSet()
    {
        for (int i = 0; i < enemyManager.childCount; i++)
        {
            mainShipInfo.FoundTarget.Add(enemyManager.GetChild(i).gameObject);
        }
    }
}
