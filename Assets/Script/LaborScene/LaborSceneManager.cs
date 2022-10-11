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
            shipNameTmp[i] = buildBtnTrf.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();//텍스트 배열에 객체 집어넣음.
            shipQtyTmp[i] = buildBtnTrf.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();//위와 같음

            ShipInfoData data = FleetManager.instance.GetShipData(i);//i 함선 데이터 불러옴

            shipNameTmp[i].text = data.shipName + "Class " + data.shipClass;//함선의 이름과 함종을 불러와서 텍스트에 입력
            shipQtyTmp[i].text = FleetManager.instance.GetFleetQtyData(i).ToString();//함선의 수량을 텍스트에 입력
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
    public Transform buildBtnTrf;//버튼 상위 트랜스폼. 버튼들의 자식 객체들에게 접근하기 위함.
    TextMeshProUGUI[] shipNameTmp = new TextMeshProUGUI[20]; //각 함선별 지정된 이름
    TextMeshProUGUI[] shipQtyTmp = new TextMeshProUGUI[20]; //각 함선별 수량

    public void BuildWdwTogle(bool value)//함선 건조 버튼을 눌렀을 때 실행. bool이 true일 경우 창이 켜지고 false이면 창이 꺼짐
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
