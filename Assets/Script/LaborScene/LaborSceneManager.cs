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
            ShipInfoData data = FleetManager.instance.GetShipData(i);//i 함선 데이터 불러옴

            shipNameTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//텍스트 배열에 객체 집어넣음.
            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//위와 같음            

            // 함선의 이름과 함선의 수량을 텍스트에 입력
            SetShipName(
                shipNameTmp[i],
                shipQtyTmp[i],
                i);

            // 함대 편성 세팅
            // 함선의 이름과 함선의 수량을 텍스트에 입력
            SetShipName(
                shipSelectBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>(),
                shipSelectBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>(), 
                i); 
        }
    }

    public void SetShipName(TextMeshProUGUI name, TextMeshProUGUI qty, int id_)
    {
        ShipInfoData data = FleetManager.instance.GetShipData(id_);//i 함선 데이터 불러옴

        name.text = data.shipName + "Class " + data.shipClass;//함선의 이름과 함종을 불러와서 텍스트에 입력
        qty.text = FleetManager.instance.GetFleetQtyData(id_).ToString();//함선의 수량을 텍스트에 입력
    }

    public Transform buildBtnTrf;//버튼 상위 트랜스폼. 버튼들의 자식 객체들에게 접근하기 위함.
    public Transform shipSelectBtnTrf;
    TextMeshProUGUI[] shipNameTmp = new TextMeshProUGUI[20]; //각 함선별 지정된 이름
    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[20]; //각 함선별 수량


    public GameObject buildWdw;
    public GameObject fleetFormationWdw;
    public GameObject shipSelectWdw;

    public void BuildWdwToggle(bool value)//함선 건조 버튼을 눌렀을 때 실행. bool이 true일 경우 창이 켜지고 false이면 창이 꺼짐
    {
        buildWdw.SetActive(value);

        if(value)//빌드창을 불러올 때, 플릿매니저에서 함대 수량 데이터를 같이 불러옴
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
}
