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
        // 함대 건조
        for (int i = 0; i < 20; i++)
        {
            shipNameTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//텍스트 배열에 객체 집어넣음.
            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//위와 같음            

            // 함선의 이름과 함선의 수량을 텍스트에 입력
            SetShipName(shipNameTmp[i], i);
            SetShipQty(shipQtyTmp[i], i);
        }
    }

    public void RefreshShipSelectBtns()
    {
        // 함대 편성 세팅
        for (int i = 0; i < 20; i++)
        {
            // 함선의 이름과 함선의 수량을 텍스트에 입력
            SetShipName(shipSelectBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>(), i);
            SetShipQty(shipSelectBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>(), i);
        }
    }

    public void SetShipName(TextMeshProUGUI name, int id_)
    {
        ShipInfoData data = FleetManager.instance.GetShipData(id_);//i 함선 데이터 불러옴
        name.text = data.shipName + "Class " + data.shipClass;//함선의 이름과 함종을 불러와서 텍스트에 입력
    }

    public void SetShipQty(TextMeshProUGUI qty, int id_)
    {
        qty.text = FleetManager.instance.GetFleetQtyData(id_).ToString();//함선의 수량을 텍스트에 입력
    }
    
    TextMeshProUGUI[] shipNameTmp = new TextMeshProUGUI[20]; //각 함선별 지정된 이름
    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[20]; //각 함선별 수량


    public GameObject buildWdw;
    public GameObject fleetFormationWdw;
    public GameObject shipSelectWdw;
    public GameObject shipInfoWdw;

    public void BuildWdwToggle(bool value)//함선 건조 버튼을 눌렀을 때 실행. bool이 true일 경우 창이 켜지고 false이면 창이 꺼짐
    {
        buildWdw.SetActive(value);

        if(value)//빌드창을 불러올 때, 플릿매니저에서 함대 수량 데이터를 같이 불러옴
        {
            for (int i = 0; i < 20; i++)
            {
                SetShipQty(shipQtyTmp[i], i);
            }
        }
    }

    public void ffWdwToggle(bool value)//플릿 포메이션 윈도우 토글
    {
        fleetFormationWdw.SetActive(value);
    }

    public void ShipSelectWdwToggle(bool value)//함선 선택 윈도우 토글
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

            shipDatas[0].text = shipNameTmp[index].text;//이름
            shipDatas[1].text = FleetManager.instance.GetShipData(index).cost.ToString();//가격
            shipDatas[2].text = FleetManager.instance.GetShipData(index).dmg.ToString() + " (" + FleetManager.instance.GetShipData(index).dmgType.ToString() + ")";//공격력과 속성
            shipDatas[3].text = FleetManager.instance.GetShipData(index).fireDelay.ToString();//공격 속도
            shipDatas[4].text = FleetManager.instance.GetShipData(index).maxRange.ToString();//최대사거리
            shipDatas[5].text = FleetManager.instance.GetShipData(index).hp.ToString();//체력
            shipDatas[6].text = FleetManager.instance.GetShipData(index).df.ToString();//방어력
            shipDatas[7].text = FleetManager.instance.GetShipData(index).sd.ToString();//보호막
            shipDatas[8].text = FleetManager.instance.GetShipData(index).defaultspeed.ToString();//속도
            shipDatas[9].text = FleetManager.instance.GetShipData(index).agility.ToString();//기동성

            switch(index)
            {
                case 0:
                    shipExplainTmp.text = " 염가형 초계함. 레이저 고정 주포를 가지고 있으며, 저렴한 제작 단가와 빠른 기동성 및 근접 화력이 강점이다.";
                    break;
                case 1:
                    shipExplainTmp.text = " 중거리 화력 지원형 초계함. 건보트라고도 불리우는 형태로써, 크기에 비해 강력한 함포를 보유하고 있어 화력 지원에 적합하다.";
                    break;
                case 2:
                    shipExplainTmp.text = " 큰 함선을 파괴하기 위해 수직 미사일 발사대를 장착한 초계함. 긴 사거리를 가지고 있다.";
                    break;
                case 3:
                    shipExplainTmp.text = " 초계함들을 상대로 우위를 점하기 위해 제작된 중대형 호위함. 보다 막강한 화력과 방어력을 통해서 전선을 형성하는 데 유리하다.";
                    break;
                default:
                    shipExplainTmp.text = "미구현된 함선입니다.";
                    break;
            }
        }
    }

    public void ShipAdd(int index)//함선 생산 버튼을 눌렀을 때 실행. 누른 버튼에 따라서 무슨 함선을 만들지 고름.
    {
        int useCost = FleetManager.instance.GetShipData(index).cost;

        if(CurrencyManager.instance.CheckCurrency(CURRENCY_TYPE.Mineral, useCost))
        {
            FleetManager.instance.AddFleetData(index, 1);
            shipQtyTmp[index].text = FleetManager.instance.GetFleetQtyData(index).ToString();

            Debug.Log("함선이 건조되었습니다");
        }
        else
        {
            Debug.Log("광물이 더 필요합니다");
        }
    }

    public void SmMoney()
    {
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Mineral, 10000);
        CurrencyManager.instance.AddCurrency(CURRENCY_TYPE.Debri, 1000);
    }
}
