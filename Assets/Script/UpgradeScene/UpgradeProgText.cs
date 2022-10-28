using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProgText : MonoBehaviour
{
    public UPGRADE_TYPE type;
    public TMPro.TMP_Text txt;
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

        // �Դ� ������ �ٸ��� ó��
        switch (type)
        {
            case UPGRADE_TYPE.NONE:
                txt.text = "WTF is this type of value doing here?";
                break;
            case UPGRADE_TYPE.FLEET:
                txt.text = string.Format("{0} lv", lv + 1);
                break;
            default:
                txt.text = string.Format("{0} lv / ���� {1} + ", lv, total);
                break;
        }

    }
}
