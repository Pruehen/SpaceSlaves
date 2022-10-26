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
    public TextMeshProUGUI goTextQty;
    public TextMeshProUGUI goTextFleetName;
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

    public void Refresh()
    {
        var num = FleetFormationManager.instance.GetFleetQty(id);
        var shipidx= FleetFormationManager.instance.GetFleetShipIdx(id);

        if (goShipImage != null)
        {
            goShipImage.SetActive(num > 0);
        }
        if (goTextQty != null)
        {
            goTextQty.text = num.ToString();
        }
        if (goTextFleetName != null)
        {
            goTextFleetName.text = string.Format("Á¦{0} Àü´ë", id + 1);
        }
    }
}
