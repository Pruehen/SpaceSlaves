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
        for (int i = 0; i < 20; i++)
        {
            ShipInfoData data = FleetManager.instance.GetShipData(i);//i �Լ� ������ �ҷ���

            shipNameTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//�ؽ�Ʈ �迭�� ��ü �������.
            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//���� ����            

            // �Լ��� �̸��� �Լ��� ������ �ؽ�Ʈ�� �Է�
            SetShipName(
                shipNameTmp[i],
                shipQtyTmp[i],
                i);

            // �Դ� �� ����
            // �Լ��� �̸��� �Լ��� ������ �ؽ�Ʈ�� �Է�
            SetShipName(
                shipSelectBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>(),
                shipSelectBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>(), 
                i); 
        }
    }

    public void SetShipName(TextMeshProUGUI name, TextMeshProUGUI qty, int id_)
    {
        ShipInfoData data = FleetManager.instance.GetShipData(id_);//i �Լ� ������ �ҷ���

        name.text = data.shipName + "Class " + data.shipClass;//�Լ��� �̸��� ������ �ҷ��ͼ� �ؽ�Ʈ�� �Է�
        qty.text = FleetManager.instance.GetFleetQtyData(id_).ToString();//�Լ��� ������ �ؽ�Ʈ�� �Է�
    }

    public Transform buildBtnTrf;//��ư ���� Ʈ������. ��ư���� �ڽ� ��ü�鿡�� �����ϱ� ����.
    public Transform shipSelectBtnTrf;
    TextMeshProUGUI[] shipNameTmp = new TextMeshProUGUI[20]; //�� �Լ��� ������ �̸�
    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[20]; //�� �Լ��� ����


    public GameObject buildWdw;
    public GameObject fleetFormationWdw;
    public GameObject shipSelectWdw;

    public void BuildWdwToggle(bool value)//�Լ� ���� ��ư�� ������ �� ����. bool�� true�� ��� â�� ������ false�̸� â�� ����
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

    public void ffWdwToggle(bool value)
    {
        fleetFormationWdw.SetActive(value);
    }

    public void ShipSelectWdwToggle(bool value)
    {
        shipSelectWdw.SetActive(value);
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
