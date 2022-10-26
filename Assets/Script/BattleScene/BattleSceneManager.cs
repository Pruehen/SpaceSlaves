using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager instance;
    //public bool isClicked { set; get; }

    private void Awake()
    {
        instance = this;
    }


    public Transform EnemyManager;
    public Transform FriendlyManager;
    public Transform DestroyedShip;

    public Transform FriendlyFleetSpawnCenter;

    public void edUp_Test()
    {
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, 100);
        Debug.Log("테스트용 함수 작동");
    }

    // deprecated
    int[] positioningShipId = { 0, 1, -1, -1, -1 };

    int selectedStage;
    private void Start()
    {
        selectedStage = StageManager.instance.selectedStage;

        InvokeRepeating("GameEndCheck", 5, 5);
        this.GetComponent<ShipSpownSystem>().FriendlyShipSpown(FriendlyFleetSpawnCenter);
        this.GetComponent<ShipSpownSystem>().EnemyShipSpown(EnemyManager, selectedStage);
        //this.GetComponent<ShipSpownSystem>().FriendlyShipSpown(positioningShipId);
    }
    
    public void GameEndCheck()
    {
        //Debug.Log(EnemyManager.childCount);
        //Debug.Log(DestroyedShip.childCount);
        if (EnemyManager.GetChild(0).childCount == 0)//enemymanager 내부에 있는 프리팹 모체 자식들을 탐색
        {
            GameEnd(true);            
        }
        else if(FriendlyManager.childCount == 0)
        {
            GameEnd(false);            
        }
    }

    public void GameEnd(bool isWin)
    {
        Time.timeScale = 0;

        if(isWin)
        {
            MessageManager.instance.PopupOk("게임 승리!", gameObject);
            StageManager.instance.GetStageData(selectedStage).isClear = true;
        }
        else
        {
            MessageManager.instance.PopupOk("게임 패배!", gameObject);
        }
    }
}

public enum battlePosition
{
    assault,
    intercepte,
    line,
    artillery,
    long_Range
}
