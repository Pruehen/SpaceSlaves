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
        Debug.Log("�׽�Ʈ�� �Լ� �۵�");
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
        if (EnemyManager.GetChild(0).childCount == 0)//enemymanager ���ο� �ִ� ������ ��ü �ڽĵ��� Ž��
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
            MessageManager.instance.PopupOk("���� �¸�!", gameObject);
            StageManager.instance.GetStageData(selectedStage).isClear = true;
        }
        else
        {
            MessageManager.instance.PopupOk("���� �й�!", gameObject);
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
