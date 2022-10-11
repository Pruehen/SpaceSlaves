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
        var total = UpgradeManager.instance.GetTotalActiveVal(type);
        var lv = UpgradeManager.instance.GetBestUpgradeId(type) % 1000;

        txt.text = string.Format("{0} lv / รัวี {1} + ", lv, total);
    }
}
