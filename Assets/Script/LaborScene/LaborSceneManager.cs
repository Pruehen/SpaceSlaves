using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LaborSceneManager : MonoBehaviour
{
    public static LaborSceneManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("DefenceScene");
    }

    public TextMeshProUGUI minText;
    public void SetMinUI()
    {
        minText.text = CurrencyManager.instance.GetCurrency(CURRENCY_TYPE.Mineral).ToString();
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
        FleetManager.instance.AddFleetData(index + 10, 1);
        fleetQty[index].text = FleetManager.instance.GetFleetQtyData(index + 10).ToString();

        Debug.Log("�Լ��� �����Ǿ����״�!");
    }
}
