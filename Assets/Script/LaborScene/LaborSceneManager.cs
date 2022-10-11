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
            shipNameTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//�ؽ�Ʈ �迭�� ��ü �������.
            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//���� ����

            ShipInfoData data = FleetManager.instance.GetShipData(i);//i �Լ� ������ �ҷ���

            shipNameTmp[i].text = data.shipName + "Class " + data.shipClass;//�Լ��� �̸��� ������ �ҷ��ͼ� �ؽ�Ʈ�� �Է�
            shipQtyTmp[i].text = FleetManager.instance.GetFleetQtyData(i).ToString();//�Լ��� ������ �ؽ�Ʈ�� �Է�
        }

        InvokeRepeating("SetMinUI", 0, 0.5f);
        InvokeRepeating("SetDebriUI", 0, 0.5f);
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
    public Transform buildBtnTrf;//��ư ���� Ʈ������. ��ư���� �ڽ� ��ü�鿡�� �����ϱ� ����.
    TextMeshProUGUI[] shipNameTmp = new TextMeshProUGUI[20]; //�� �Լ��� ������ �̸�
    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[20]; //�� �Լ��� ����

    public void BuildWdwTogle(bool value)//�Լ� ���� ��ư�� ������ �� ����. bool�� true�� ��� â�� ������ false�̸� â�� ����
    {
        buildWdw.SetActive(value);

        if(value)//����â�� �ҷ��� ��, �ø��Ŵ������� �Դ� ���� �����͸� ���� �ҷ���
        {
            for (int i = 0; i < 20; i++)
            {                
                shipQtyTmp[i].text = FleetManager.instance.GetFleetQtyData(i).ToString();
            }
        }
    }

    public void ShipAdd(int index)//�Լ� ���� ��ư�� ������ �� ����. ���� ��ư�� ���� ���� �Լ��� ������ ��.
    {
        int useCost = FleetManager.instance.GetShipData(index).cost;

        if(CurrencyManager.instance.CheckCurrency(CURRENCY_TYPE.Mineral, useCost))
        {
            FleetManager.instance.AddFleetData(index, 1);
            shipQtyTmp[index].text = FleetManager.instance.GetFleetQtyData(index).ToString();

            Debug.Log("�Լ��� �����Ǿ����ϴ�");
        }
        else
        {
            Debug.Log("������ �� �ʿ��մϴ�");
        }

    }
}
