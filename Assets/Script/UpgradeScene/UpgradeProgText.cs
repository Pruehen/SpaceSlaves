using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProgText : MonoBehaviour
{
    public UPGRADE_TYPE type;
    public TMPro.TMP_Text infoTxt;
    public TMPro.TMP_Text priceTxt;
    public GameObject arrowUpAni;

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }
    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    public void OnClickUpgrade()
    {
        if (UpgradeManager.instance.DoBestUpgrade(type))
        {
            PopArrowUPAniI();
        }        
    }

    public void PopArrowUPAniI()
    {
        Destroy(Instantiate(arrowUpAni, transform), 0.3f);
    }

    void Refresh()
    {
        var total = UpgradeManager.GetTotalActiveVal(type);
        var bestId = UpgradeManager.instance.GetBestUpgradeId(type);
        var lv = bestId % 1000;

        if (UpgradeStaticManager.instance.IsExist(bestId))
        {
            priceTxt.text = UpgradeStaticManager.instance.GetCost(bestId).ToString();
        }
        else
        {
            priceTxt.text = "MAX";
        }

        // 함대 레벨은 다르게 처리
        switch (type)
        {
            case UPGRADE_TYPE.NONE:
                infoTxt.text = "WTF is this type of value doing here?";
                break;
            case UPGRADE_TYPE.SCV_SPEED_UP:
                infoTxt.text = string.Format("{0} lv / +{1}% ", lv, (1 + total*0.2f) * 100);
                break;
            case UPGRADE_TYPE.SCV_AMOUNT_UP:
                infoTxt.text = string.Format("{0} lv / +{1} ", lv, total);
                break;
            case UPGRADE_TYPE.COLLECTOR_CAPA:
                infoTxt.text = string.Format("{0} lv / +{1} ", lv, total);
                break;
            case UPGRADE_TYPE.FLEET:
                infoTxt.text = string.Format("{0} lv", lv + 1);
                break;
            case UPGRADE_TYPE.FLEET_CAPA:
                infoTxt.text = string.Format("{0} lv / +{1} ", lv, total);
                break;
            default:
                infoTxt.text = string.Format("{0} lv / +{1}% ", lv, (1 + total) * 100);
                break;
        }

    }
}
