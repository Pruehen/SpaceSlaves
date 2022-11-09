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

    public Transform buildBtnTrf;//버튼 상위 트랜스폼. 버튼들의 자식 객체들에게 접근하기 위함.
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

        // 함대 건조창 데이터 입력
        for (int i = 0; i < 12; i++)
        {
            targetTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//함선 이름 텍스트 위치
            SetShipName(targetTmp[i], i);//이름 입력

            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//위와 같음            
            SetShipQty(shipQtyTmp[i], i);//수량 입력

            targetTmp[i] = buildBtnTrf.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>();//함선 가격 텍스트 위치
            SetShipPrice(targetTmp[i], i);

            buildBtnTrf.GetChild(i).GetChild(2).GetComponent<Image>().sprite = FleetManager.instance.GetShipImage(i); // 함선이미지
        }
    }

    public void RefreshShipSelectBtns()
    {        
        // 함대 편성 세팅
        for (int i = 0; i < shipSelectBtnTrf.childCount; i++)
        {
            // 함선의 이름과 함선의 수량을 텍스트에 입력
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
        ShipInfoData data = FleetManager.instance.GetShipData(id_);//i 함선 데이터 불러옴

        string shipClassName = "";
        switch(data.shipClass)
        {
            case ship_Class.Corvette:
                shipClassName = "초계함";
                break;
            case ship_Class.Destroyer:
                shipClassName = "구축함";
                break;
            case ship_Class.Cruiser:
                shipClassName = "순양함";
                break;
            case ship_Class.Battleship:
                shipClassName = "전함";
                break;
        }

        name.text = data.shipName + "급\n" + shipClassName;//함선의 이름과 함종을 불러와서 텍스트에 입력
    }

    public void SetShipQty(TextMeshProUGUI qty, int id_)
    {
        qty.text = FleetManager.instance.GetFleetQtyData(id_).ToString();//함선의 수량을 텍스트에 입력
    }

    public void SetShipPrice(TextMeshProUGUI price, int id_)
    {
        price.text = FleetManager.instance.GetShipData(id_).cost.ToString();//함선의 가격을 텍스트에 입력
    }

    public GameObject buildWdw;
    public GameObject fleetFormationWdw;
    public GameObject shipSelectWdw;
    public GameObject shipInfoWdw;

    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[12]; //각 함선별 수량
    public void BuildWdwToggle(bool value)//함선 건조 버튼을 눌렀을 때 실행. bool이 true일 경우 창이 켜지고 false이면 창이 꺼짐
    {
        buildWdw.SetActive(value);

        if(value)//빌드창을 불러올 때, 플릿매니저에서 함대 수량 데이터를 같이 불러옴
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

    public void ffWdwToggle(bool value)//플릿 포메이션 윈도우 토글
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

    public void HelpWinToggle(bool value)//도움말 토글
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

    public void ShipSelectWdwToggle(bool value)//함선 선택 윈도우 토글
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

            shipDatas[0].text = FleetManager.instance.GetShipData(index).shipName;//이름
            shipDatas[1].text = FleetManager.instance.GetShipData(index).cost.ToString();//가격
            shipDatas[2].text = FleetManager.instance.GetShipData(index).dmg.ToString() + " (" + FleetManager.instance.GetShipData(index).dmgType.ToString() + ")";//공격력과 속성
            shipDatas[3].text = FleetManager.instance.GetShipData(index).fireDelay.ToString();//공격 속도
            shipDatas[4].text = FleetManager.instance.GetShipData(index).maxRange.ToString();//최대사거리
            shipDatas[5].text = FleetManager.instance.GetShipData(index).hp.ToString();//체력
            shipDatas[6].text = FleetManager.instance.GetShipData(index).df.ToString();//방어력
            shipDatas[7].text = FleetManager.instance.GetShipData(index).sd.ToString();//보호막
            shipDatas[8].text = FleetManager.instance.GetShipData(index).defaultspeed.ToString();//속도
            shipDatas[9].text = FleetManager.instance.GetShipData(index).agility.ToString();//기동성

            modelCam.CamSet(index);

            switch (index)
            {
                case 0:
                    shipExplainTmp.text = " 레이저 주포를 가진 값싼 다목적 초계함입니다.";
                    break;
                case 1:
                    shipExplainTmp.text = " 중거리 실탄 화력 지원형 초계함입니다.";
                    break;
                case 2:
                    shipExplainTmp.text = " 수직 미사일 발사대를 장착한 초계함입니다. 먼 거리에서 강력한 피해를 입힐 수 있습니다.";
                    break;
                case 3:
                    shipExplainTmp.text = " 중대형 구축함입니다. 다량의 레이저 주포를 탑재하고 있습니다.";
                    break;
                case 4:
                    shipExplainTmp.text = " 빠른 발사 속도를 가진 기관포를 탑재한 중거리 화력 지원 함선입니다.";
                    break;
                case 5:
                    shipExplainTmp.text = " 막대한 피해를 주는 어뢰를 장거리에서 발사하는 중뇌장 구축함입니다.";
                    break;
                case 6:
                    shipExplainTmp.text = " 강력한 순양함입니다. 높은 방어력과 강력한 화력을 가졌습니다.";
                    break;
                case 7:
                    shipExplainTmp.text = " 수많은 실탄 투사포를 탑재하고 있어 높은 다수전 성능을 지닌 순양함입니다.";
                    break;
                case 8:
                    shipExplainTmp.text = " 동체에 막대한 양의 미사일을 탑재한 아스널 쉽입니다. 미사일을 이용한 탄막 형성에 적합합니다.";
                    break;
                case 9:
                    shipExplainTmp.text = " 전함의 주포를 함체에 고정시켜 탑재한 강력한 중순양함입니다. 장거리에서 강력한 화력을 투사할 수 있습니다.";
                    break;
                case 10:
                    shipExplainTmp.text = " 강력한 전함입니다. 강력한 주포 포탑이 탑재되어 있으며, 근접한 적은 부포로 공격합니다.";
                    break;
                case 11:
                    shipExplainTmp.text = " 공성전에 특화된 전함입니다. 다수의 적을 관통하는 랜스를 발사해서 막대한 피해를 줄 수 있습니다.";
                    break;
                default:
                    shipExplainTmp.text = "미구현된 함선입니다.";
                    break;
            }
        }
    }

    public GameObject stageSelectWdw;
    public Transform stageBtnTrf;
    public void StageSelectWdwToggle(bool value)//스테이지 선택 윈도우 토글
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
    public void StageInfoWdwToggle(int index)//스테이지별 테이터 윈도우 토글//
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
                stageInfoText.text = stageInfoText.text + kvp.Key + "급 X" + kvp.Value + "\n";
            }
            stageStoryText.text = StageManager.instance.GetStageStoryString(index);
        }
    }

    public GameObject Smessage;

    public void ShipAdd(int index)//함선 생산 버튼을 눌렀을 때 실행. 누른 버튼에 따라서 무슨 함선을 만들지 고름.
    {
        if (UpgradeManager.instance.GetFleetLevel() - 1 < index)
        {
            Smessage.SendMessage("MessageQFade", "조선소 레벨이 부족합니다");
            Debug.Log("조선소 레벨이 부족합니다");
            SoundManager.instance.clickSoundOn();
            return;
        }

        int useCost = FleetManager.instance.GetShipData(index).cost;

        if(CurrencyManager.instance.CheckCurrency(CURRENCY_TYPE.Mineral, useCost))
        {
            FleetManager.instance.AddFleetData(index, 1);
            shipQtyTmp[index].text = FleetManager.instance.GetFleetQtyData(index).ToString();

            Debug.Log("함선이 건조되었습니다");
            SoundManager.instance.ShipBuildSoundOn();
        }
        else
        {
            Debug.Log("광물이 더 필요합니다");
            Smessage.SendMessage("MessageQFade","광물이 더 필요합니다");
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
