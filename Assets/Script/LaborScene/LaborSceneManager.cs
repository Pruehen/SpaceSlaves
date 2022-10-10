using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LaborSceneManager : MonoBehaviour
{
    public static LaborSceneManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetMinUI();
        SetDebriUI();

        for (int i = 0; i < 20; i++)
        {
            fleetQty[i].text = FleetManager.instance.GetFleetQtyData(i).ToString();
        }
    }

    public TextMeshProUGUI minText;
    public TextMeshProUGUI debriText;
    public void SetMinUI()
    {
        minText.text = CurrencyManager.instance.GetCurrency(CURRENCY_TYPE.Mineral).ToString();
    }
    public void SetDebriUI()
    {
        debriText.text = CurrencyManager.instance.GetCurrency(CURRENCY_TYPE.Debri).ToString();
    }

    public GameObject buildWdw;
    public TextMeshProUGUI[] fleetQty = new TextMeshProUGUI[20];
    public void BuildWdwTogle(bool value)
    {
        buildWdw.SetActive(value);

        if(value)//����â�� �ҷ��� ��, �ø��Ŵ������� �Դ� ���� �����͸� ���� �ҷ���
        {
            for (int i = 0; i < 20; i++)
            {
                if (fleetQty[i] != null)
                {
                    fleetQty[i].text = FleetManager.instance.GetFleetQtyData(i + 10).ToString();
                }
            }
        }
    }

    public void ShipAdd(int index)
    {
        int useCost = FleetManager.instance.GetShipCost(index);

        if(CurrencyManager.instance.CheckCurrency(CURRENCY_TYPE.Mineral, useCost))
        {
            FleetManager.instance.AddFleetData(index, 1);
            fleetQty[index].text = FleetManager.instance.GetFleetQtyData(index).ToString();

            Debug.Log("�Լ��� �����Ǿ����״�!");
        }
        else
        {
            Debug.Log("������ �� �ʿ��մϴ�!");
        }

    }
}
