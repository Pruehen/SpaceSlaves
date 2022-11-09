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
        TextMeshProUGUI[] targetTmp = new TextMeshProUGUI[12];

        // �Դ� ����â ������ �Է�
        for (int i = 0; i < 12; i++)
        {
            targetTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//�Լ� �̸� �ؽ�Ʈ ��ġ
            SetShipName(targetTmp[i], i);//�̸� �Է�

            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//���� ����            
            SetShipQty(shipQtyTmp[i], i);//���� �Է�

            targetTmp[i] = buildBtnTrf.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>();//�Լ� ���� �ؽ�Ʈ ��ġ
            SetShipPrice(targetTmp[i], i);

            buildBtnTrf.GetChild(i).GetChild(2).GetComponent<Image>().sprite = FleetManager.instance.GetShipImage(i); // �Լ��̹���
        }
    }

    public void RefreshShipSelectBtns()
    {        
        // �Դ� �� ����
        for (int i = 0; i < shipSelectBtnTrf.childCount; i++)
        {
            // �Լ��� �̸��� �Լ��� ������ �ؽ�Ʈ�� �Է�
            SetShipName(shipSelectBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>(), i);
            SetShipQty(shipSelectBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>(), i);
            shipSelectBtnTrf.GetChild(i).GetChild(2).GetComponent<Image>().sprite = FleetManager.instance.GetShipImage(i);
        }
    }

    void RefreshStageNameBtns()
    {
        for (int i = 0; i < 10; i++)
        {
            stageBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = StageManager.instance.GetStageData(i).stageName;
        }
    }

    public void SetShipName(TextMeshProUGUI name, int id_)
    {
        ShipInfoData data = FleetManager.instance.GetShipData(id_);//i �Լ� ������ �ҷ���

        string shipClassName = "";
        switch(data.shipClass)
        {
            case ship_Class.Corvette:
                shipClassName = "�ʰ���";
                break;
            case ship_Class.Destroyer:
                shipClassName = "������";
                break;
            case ship_Class.Cruiser:
                shipClassName = "������";
                break;
            case ship_Class.Battleship:
                shipClassName = "����";
                break;
        }

        name.text = data.shipName + "��\n" + shipClassName;//�Լ��� �̸��� ������ �ҷ��ͼ� �ؽ�Ʈ�� �Է�
    }

    public void SetShipQty(TextMeshProUGUI qty, int id_)
    {
        qty.text = FleetManager.instance.GetFleetQtyData(id_).ToString();//�Լ��� ������ �ؽ�Ʈ�� �Է�
    }

    public void SetShipPrice(TextMeshProUGUI price, int id_)
    {
        price.text = FleetManager.instance.GetShipData(id_).cost.ToString();//�Լ��� ������ �ؽ�Ʈ�� �Է�
    }

    public GameObject buildWdw;
    public GameObject fleetFormationWdw;
    public GameObject shipSelectWdw;
    public GameObject shipInfoWdw;

    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[12]; //�� �Լ��� ����
    public void BuildWdwToggle(bool value)//�Լ� ���� ��ư�� ������ �� ����. bool�� true�� ��� â�� ������ false�̸� â�� ����
    {
        buildWdw.SetActive(value);

        if(value)//����â�� �ҷ��� ��, �ø��Ŵ������� �Դ� ���� �����͸� ���� �ҷ���
        {
            SoundManager.instance.clickSoundOn();
            for (int i = 0; i < 12; i++)
            {
                SetShipQty(shipQtyTmp[i], i);
            }            
        }
        else
        {
            SoundManager.instance.CloseSoundOn();
        }
    }

    public void ffWdwToggle(bool value)//�ø� �����̼� ������ ���
    {
        fleetFormationWdw.SetActive(value);
        if(value)
        {
            SoundManager.instance.clickSoundOn();
        }
        else
        {
            SoundManager.instance.CloseSoundOn();
        }   
        
    }

    public GameObject HelpWin;

    public void HelpWinToggle(bool value)//���� ���
    {
        HelpWin.SetActive(value);
        if(value)
        {
            SoundManager.instance.clickSoundOn();
        }
        else
        {
            SoundManager.instance.CloseSoundOn();
        }    
    }

    public void ShipSelectWdwToggle(bool value)//�Լ� ���� ������ ���
    {
        shipSelectWdw.SetActive(value);
        if(value)
        {
            SoundManager.instance.clickSoundOn();
            for (int i = 0; i < 12; i++)
            {
                SetShipQty(shipSelectBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>(), i);
            }
        }
        else
        {
            SoundManager.instance.CloseSoundOn();
        }
    }

    public TextMeshProUGUI[] shipDatas = new TextMeshProUGUI[10];
    public TextMeshProUGUI shipExplainTmp;
    public ModelCam modelCam;
    public void ShipInfoWdwToggle(int index)
    {
        if(index == -1)
        {
            shipInfoWdw.SetActive(false);
            SoundManager.instance.CloseSoundOn();

            modelCam.CamSetOff();
        }
        else
        {
            shipInfoWdw.SetActive(true);
            SoundManager.instance.clickSoundOn();

            shipDatas[0].text = FleetManager.instance.GetShipData(index).shipName;//�̸�
            shipDatas[1].text = FleetManager.instance.GetShipData(index).cost.ToString();//����
            shipDatas[2].text = FleetManager.instance.GetShipData(index).dmg.ToString() + " (" + FleetManager.instance.GetShipData(index).dmgType.ToString() + ")";//���ݷ°� �Ӽ�
            shipDatas[3].text = FleetManager.instance.GetShipData(index).fireDelay.ToString();//���� �ӵ�
            shipDatas[4].text = FleetManager.instance.GetShipData(index).maxRange.ToString();//�ִ��Ÿ�
            shipDatas[5].text = FleetManager.instance.GetShipData(index).hp.ToString();//ü��
            shipDatas[6].text = FleetManager.instance.GetShipData(index).df.ToString();//����
            shipDatas[7].text = FleetManager.instance.GetShipData(index).sd.ToString();//��ȣ��
            shipDatas[8].text = FleetManager.instance.GetShipData(index).defaultspeed.ToString();//�ӵ�
            shipDatas[9].text = FleetManager.instance.GetShipData(index).agility.ToString();//�⵿��

            modelCam.CamSet(index);

            switch (index)
            {
                case 0:
                    shipExplainTmp.text = " ������ ������ ���� ���� �ٸ��� �ʰ����Դϴ�.";
                    break;
                case 1:
                    shipExplainTmp.text = " �߰Ÿ� ��ź ȭ�� ������ �ʰ����Դϴ�.";
                    break;
                case 2:
                    shipExplainTmp.text = " ���� �̻��� �߻�븦 ������ �ʰ����Դϴ�. �� �Ÿ����� ������ ���ظ� ���� �� �ֽ��ϴ�.";
                    break;
                case 3:
                    shipExplainTmp.text = " �ߴ��� �������Դϴ�. �ٷ��� ������ ������ ž���ϰ� �ֽ��ϴ�.";
                    break;
                case 4:
                    shipExplainTmp.text = " ���� �߻� �ӵ��� ���� ������� ž���� �߰Ÿ� ȭ�� ���� �Լ��Դϴ�.";
                    break;
                case 5:
                    shipExplainTmp.text = " ������ ���ظ� �ִ� ��ڸ� ��Ÿ����� �߻��ϴ� �߳��� �������Դϴ�.";
                    break;
                case 6:
                    shipExplainTmp.text = " ������ �������Դϴ�. ���� ���°� ������ ȭ���� �������ϴ�.";
                    break;
                case 7:
                    shipExplainTmp.text = " ������ ��ź �������� ž���ϰ� �־� ���� �ټ��� ������ ���� �������Դϴ�.";
                    break;
                case 8:
                    shipExplainTmp.text = " ��ü�� ������ ���� �̻����� ž���� �ƽ��� ���Դϴ�. �̻����� �̿��� ź�� ������ �����մϴ�.";
                    break;
                case 9:
                    shipExplainTmp.text = " ������ ������ ��ü�� �������� ž���� ������ �߼������Դϴ�. ��Ÿ����� ������ ȭ���� ������ �� �ֽ��ϴ�.";
                    break;
                case 10:
                    shipExplainTmp.text = " ������ �����Դϴ�. ������ ���� ��ž�� ž��Ǿ� ������, ������ ���� ������ �����մϴ�.";
                    break;
                case 11:
                    shipExplainTmp.text = " �������� Ưȭ�� �����Դϴ�. �ټ��� ���� �����ϴ� ������ �߻��ؼ� ������ ���ظ� �� �� �ֽ��ϴ�.";
                    break;
                default:
                    shipExplainTmp.text = "�̱����� �Լ��Դϴ�.";
                    break;
            }
        }
    }

    public GameObject stageSelectWdw;
    public Transform stageBtnTrf;
    public void StageSelectWdwToggle(bool value)//�������� ���� ������ ���
    {
        RefreshStageNameBtns();
        stageSelectWdw.SetActive(value);

        if (value)
        {
            SoundManager.instance.clickSoundOn();
        }
        else
        {
            SoundManager.instance.CloseSoundOn();
        }
    }


    public GameObject stageInfoWdw;
    public TextMeshProUGUI stageInfoText;
    public TextMeshProUGUI stageStoryText;
    public void StageInfoWdwToggle(int index)//���������� ������ ������ ���//
    {
        if (index == -1)
        {
            stageInfoWdw.SetActive(false);
            SoundManager.instance.CloseSoundOn();
        }
        else
        {
            stageInfoText.text = null;

            SoundManager.instance.clickSoundOn();

            stageInfoWdw.SetActive(true);
            StageManager.instance.selectedStage = index;

            Dictionary<string, int> fleetData = StageManager.instance.GetStageFleetData(index);

            foreach (KeyValuePair<string, int> kvp in fleetData)
            {
                stageInfoText.text = stageInfoText.text + kvp.Key + "�� X" + kvp.Value + "\n";
            }
            stageStoryText.text = StageManager.instance.GetStageStoryString(index);
        }
    }

    public GameObject Smessage;

    public void ShipAdd(int index)//�Լ� ���� ��ư�� ������ �� ����. ���� ��ư�� ���� ���� �Լ��� ������ ��.
    {
        if (UpgradeManager.instance.GetFleetLevel() - 1 < index)
        {
            Smessage.SendMessage("MessageQFade", "������ ������ �����մϴ�");
            Debug.Log("������ ������ �����մϴ�");
            SoundManager.instance.clickSoundOn();
            return;
        }

        int useCost = FleetManager.instance.GetShipData(index).cost;

        if(CurrencyManager.instance.CheckCurrency(CURRENCY_TYPE.Mineral, useCost))
        {
            FleetManager.instance.AddFleetData(index, 1);
            shipQtyTmp[index].text = FleetManager.instance.GetFleetQtyData(index).ToString();

            Debug.Log("�Լ��� �����Ǿ����ϴ�");
            SoundManager.instance.ShipBuildSoundOn();
        }
        else
        {
            Debug.Log("������ �� �ʿ��մϴ�");
            Smessage.SendMessage("MessageQFade","������ �� �ʿ��մϴ�");
            SoundManager.instance.clickSoundOn();
        }
    }

    public void SmMoney()
    {
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, 1000000);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, 100000);
    }
    public void UgrdReset()
    {

    }
}
