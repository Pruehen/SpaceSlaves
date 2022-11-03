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

    public DmgTextManager DmgTextManager;

    public void SetDmgTmp(Transform targetTrf, int text, DmgTextType dmgTextType)
    {
        DmgTextManager.SetDmgTmp(targetTrf, text, dmgTextType);
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

    float collectedDebri = 0;
    float collectedMin = 0;
    public void AddCurrency(float id, int shipClass)
    {
        int getCur = (int)(id * shipClass * shipClass * shipClass) + 2;

        collectedDebri += getCur;
        collectedMin += getCur * 5;
    }

    public void GameEnd(bool isWin)
    {
        Time.timeScale = 0;

        int[] debugArray = new int[5];

        debriefingWdw.gameObject.SetActive(true);

        //debriefingWdw.SetTmp_LostShip(debugArray);
        //debriefingWdw.SetTmp_ShipDmg(debugArray);

        if (isWin)
        {
            collectedMin *= 1.5f;
            collectedDebri *= 1.5f;

            //MessageManager.instance.PopupOk("게임 승리!", gameObject);
            debriefingWdw.winLoseTmp.text = "게임 승리!";
            StageManager.instance.GetStageData(selectedStage).isClear = true;
        }
        else
        {
            debriefingWdw.winLoseTmp.text = "게임 패배!";
            //MessageManager.instance.PopupOk("게임 패배!", gameObject);
        }

        debriefingWdw.SetTmp_GetCurrency((int)collectedMin, (int)collectedDebri);

        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, (int)collectedDebri);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, (int)collectedMin);
    }

    public DebriefingWdw debriefingWdw;
}

public enum battlePosition
{
    assault,
    intercepte,
    line,
    artillery,
    long_Range
}
