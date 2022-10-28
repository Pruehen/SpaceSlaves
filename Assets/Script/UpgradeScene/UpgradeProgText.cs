using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProgText : MonoBehaviour
{
    public UPGRADE_TYPE type;
    public TMPro.TMP_Text infoTxt;
    public TMPro.TMP_Text priceTxt;
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

    void Refresh()
    {
        var total = UpgradeManager.GetTotalActiveVal(type);
        var lv = UpgradeManager.instance.GetBestUpgradeId(type) % 1000;

        priceTxt.text = UpgradeStaticManager.instance.GetCost(UpgradeManager.instance.GetBestUpgradeId(type)).ToString();
        // 함대 레벨은 다르게 처리
        switch (type)
        {
            case UPGRADE_TYPE.NONE:
                infoTxt.text = "WTF is this type of value doing here?";
                break;
            case UPGRADE_TYPE.FLEET:
                infoTxt.text = string.Format("{0} lv", lv + 1);
                break;
            default:
                infoTxt.text = string.Format("{0} lv / 총합 {1} + ", lv, total);
                break;
        }

    }
}
