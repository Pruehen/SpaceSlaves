using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{

    public void ResetAllUpgrade()
    {
        UpgradeManager.instance.TestReset();
    }
}
