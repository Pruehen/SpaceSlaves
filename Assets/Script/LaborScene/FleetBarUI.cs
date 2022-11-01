using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleetBarUI : MonoBehaviour
{
    public int id = -1;

    public GameObject goShipImage;
    public GameObject goWarningIcon;
    public Button goEmptyBtn; 
    public TextMeshProUGUI goTextQty;
    public TextMeshProUGUI goTextFleetName;

    private void Start()
    {
        //goEmptyBtn.onClick.AddListener(delegate() 
        //{ 
        //    OnClickEmpty();
        //});
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        Refresh();
    }

    public void SetWarningIcon(bool isSet) 
    {
        if (goWarningIcon != null)
            goWarningIcon.SetActive(isSet);
    }

    public void OnClickEmpty()
    {
        if (FleetFormationManager.instance.RemoveUnit(id))
        {
            Debug.Log("비워짐 " + id);
        }
    }

    public void Refresh()
    {
        var qty = FleetFormationManager.instance.GetFleetQty(id);
        var num = FleetFormationManager.instance.GetFleetCost(id);
        var maxNum = FleetFormationManager.instance.GetFleetMaxSize(id);
        var shipidx= FleetFormationManager.instance.GetFleetShipIdx(id);

        if (goShipImage != null)
        {
            bool isActive = num > 0;
            goShipImage.SetActive(isActive);
            if (isActive)
                goShipImage.GetComponent<Image>().sprite = FleetManager.instance.GetShipImage(shipidx);
        }
        if (goTextQty != null)
        {
            goTextQty.text = qty.ToString() + "대 :" + num.ToString() + "/" + maxNum.ToString();
        }
        if (goTextFleetName != null)
        {
            goTextFleetName.text = string.Format("제{0} 전대", id + 1);
        }
    }
}
