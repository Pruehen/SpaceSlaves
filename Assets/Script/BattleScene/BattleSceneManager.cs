using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform EnemyManager;
    public Transform FriendlyManager;

    public void edUp_Test()
    {
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, 100);
        Debug.Log("테스트용 함수 작동");
    }

    int[] positioningShipId = { 0, -1, -1, -1, -1 };

    private void Start()
    {
        this.GetComponent<ShipSpownSystem>().FriendlyShipSpown(positioningShipId);
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
