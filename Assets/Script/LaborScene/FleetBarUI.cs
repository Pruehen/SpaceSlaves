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
        FleetFormationManager.instance.RemoveUnit(id);
    }

    public void Refresh()
    {
        var num = FleetFormationManager.instance.GetFleetQty(id);
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
            goTextQty.text = num.ToString();
        }
        if (goTextFleetName != null)
        {
            goTextFleetName.text = string.Format("��{0} ����", id + 1);
        }
    }
}
