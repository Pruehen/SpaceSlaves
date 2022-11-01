using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebriefingWdw : MonoBehaviour
{
    public TextMeshProUGUI winLoseTmp;
    public TextMeshProUGUI[] getCurrencyTmp = new TextMeshProUGUI[2];
    public TextMeshProUGUI[] lostShipTmp = new TextMeshProUGUI[5];
    public TextMeshProUGUI[] shipDmgTmp = new TextMeshProUGUI[5];

    int[] formationShipIndexArray = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            formationShipIndexArray[i] = FleetFormationManager.instance.GetFleetShipIdx(i);
        }

        /*for (int i = 0; i < 5; i++)//함선 관련 tmp 초기화 작업
        {
            if (formationShipIndexArray[i] != -1)
            {
                lostShipTmp[i].text = FleetManager.instance.GetShipName(formationShipIndexArray[i]) + "급 ";
                shipDmgTmp[i].text = FleetManager.instance.GetShipName(formationShipIndexArray[i]) + "급 ";
            }
        }*/
    }

    public void SetTmp_GetCurrency(int min, int debri)
    {
        getCurrencyTmp[0].text = "+" + min;
        getCurrencyTmp[1].text = "+" + debri;
    }
    

    /*public void SetTmp_LostShip(int[] lostShipArray)
    {
        for (int i = 0; i < 5; i++)
        {
            if (lostShipArray[i] != 0)
            {
                lostShipTmp[i].text += lostShipArray[i] + "척 손실";
            }
        }   
    }

    public void SetTmp_ShipDmg(int[] ShipDmgArray)
    {
        for (int i = 0; i < 5; i++)
        {
            if (ShipDmgArray[i] != 0)
            {
                shipDmgTmp[i].text += "피해량 + " + ShipDmgArray[i];
            }
        }
    }*/
}
