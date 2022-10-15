using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleetBarUI : MonoBehaviour
{
    public int id = -1;

    public GameObject goShipImage;
    public TextMeshProUGUI goTextQty;
    public TextMeshProUGUI goTextFleetName;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (goShipImage != null)
        {

        }

        if (goTextQty != null)
        {
            var num = FleetFormationManager.instance.GetFleetQty(id);
            goTextQty.text = num.ToString();
        }
        if (goTextFleetName != null)
        {
            goTextFleetName.text = string.Format("Á¦{0} Àü´ë", id + 1);
        }
    }
}
