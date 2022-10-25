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

    public Transform buildBtnTrf;//��ư ���� Ʈ������. ��ư���� �ڽ� ��ü�鿡�� �����ϱ� ����.
    public Transform shipSelectBtnTrf;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        RefreshShipBuyBtns();
        RefreshShipSelectBtns();
    }

    public void RefreshShipBuyBtns()
    {
        // �Դ� ����
        for (int i = 0; i < 20; i++)
        {
            shipNameTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//�ؽ�Ʈ �迭�� ��ü �������.
            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//���� ����            

            // �Լ��� �̸��� �Լ��� ������ �ؽ�Ʈ�� �Է�
            SetShipName(shipNameTmp[i], i);
            SetShipQty(shipQtyTmp[i], i);
        }
    }

    public void RefreshShipSelectBtns()
    {
        // �Դ� �� ����
        for (int i = 0; i < 20; i++)
        {
            // �Լ��� �̸��� �Լ��� ������ �ؽ�Ʈ�� �Է�
            SetShipName(shipSelectBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>(), i);
            SetShipQty(shipSelectBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>(), i);
        }
    }

    public void SetShipName(TextMeshProUGUI name, int id_)
    {
        ShipInfoData data = FleetManager.instance.GetShipData(id_);//i �Լ� ������ �ҷ���
        name.text = data.shipName + "Class " + data.shipClass;//�Լ��� �̸��� ������ �ҷ��ͼ� �ؽ�Ʈ�� �Է�
    }

    public void SetShipQty(TextMeshProUGUI qty, int id_)
    {
        qty.text = FleetManager.instance.GetFleetQtyData(id_).ToString();//�Լ��� ������ �ؽ�Ʈ�� �Է�
    }
    
    TextMeshProUGUI[] shipNameTmp = new TextMeshProUGUI[20]; //�� �Լ��� ������ �̸�
    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[20]; //�� �Լ��� ����


    public GameObject buildWdw;
    public GameObject fleetFormationWdw;
    public GameObject shipSelectWdw;
    public GameObject shipInfoWdw;

    public void BuildWdwToggle(bool value)//�Լ� ���� ��ư�� ������ �� ����. bool�� true�� ��� â�� ������ false�̸� â�� ����
    {
        buildWdw.SetActive(value);

        if(value)//����â�� �ҷ��� ��, �ø��Ŵ������� �Դ� ���� �����͸� ���� �ҷ���
        {
            for (int i = 0; i < 20; i++)
            {
                SetShipQty(shipQtyTmp[i], i);
            }
        }
    }

    public void ffWdwToggle(bool value)//�ø� �����̼� ������ ���
    {
        fleetFormationWdw.SetActive(value);
    }

    public void ShipSelectWdwToggle(bool value)//�Լ� ���� ������ ���
    {
        shipSelectWdw.SetActive(value);
        if(value)
        {
            for (int i = 0; i < 20; i++)
            {
                SetShipQty(shipSelectBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>(), i);
            }
        }
    }

    public TextMeshProUGUI[] shipDatas = new TextMeshProUGUI[10];
    public TextMeshProUGUI shipExplainTmp;
    public void ShipInfoWdwToggle(int index)
    {
        if(index == -1)
        {
            shipInfoWdw.SetActive(false);
        }
        else
        {
            shipInfoWdw.SetActive(true);

            shipDatas[0].text = shipNameTmp[index].text;//�̸�
            shipDatas[1].text = FleetManager.instance.GetShipData(index).cost.ToString();//����
            shipDatas[2].text = FleetManager.instance.GetShipData(index).dmg.ToString() + " (" + FleetManager.instance.GetShipData(index).dmgType.ToString() + ")";//���ݷ°� �Ӽ�
            shipDatas[3].text = FleetManager.instance.GetShipData(index).fireDelay.ToString();//���� �ӵ�
            shipDatas[4].text = FleetManager.instance.GetShipData(index).maxRange.ToString();//�ִ��Ÿ�
            shipDatas[5].text = FleetManager.instance.GetShipData(index).hp.ToString();//ü��
            shipDatas[6].text = FleetManager.instance.GetShipData(index).df.ToString();//����
            shipDatas[7].text = FleetManager.instance.GetShipData(index).sd.ToString();//��ȣ��
            shipDatas[8].text = FleetManager.instance.GetShipData(index).defaultspeed.ToString();//�ӵ�
            shipDatas[9].text = FleetManager.instance.GetShipData(index).agility.ToString();//�⵿��

            switch(index)
            {
                case 0:
                    shipExplainTmp.text = " ������ �ʰ���. ������ ���� ������ ������ ������, ������ ���� �ܰ��� ���� �⵿�� �� ���� ȭ���� �����̴�.";
                    break;
                case 1:
                    shipExplainTmp.text = " �߰Ÿ� ȭ�� ������ �ʰ���. �Ǻ�Ʈ��� �Ҹ���� ���·ν�, ũ�⿡ ���� ������ ������ �����ϰ� �־� ȭ�� ������ �����ϴ�.";
                    break;
                case 2:
                    shipExplainTmp.text = " ū �Լ��� �ı��ϱ� ���� ���� �̻��� �߻�븦 ������ �ʰ���. �� ��Ÿ��� ������ �ִ�.";
                    break;
                case 3:
                    shipExplainTmp.text = " �ʰ��Ե��� ���� ������ ���ϱ� ���� ���۵� �ߴ��� ȣ����. ���� ������ ȭ�°� ������ ���ؼ� ������ �����ϴ� �� �����ϴ�.";
                    break;
                default:
                    shipExplainTmp.text = "�̱����� �Լ��Դϴ�.";
                    break;
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

    public void SmMoney()
    {
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, 10000);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, 1000);
    }
}
