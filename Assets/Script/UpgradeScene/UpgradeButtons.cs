using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public void OnClickUpgrade(int id)
    {
        UpgradeManager.instance.DoBestUpgrade(id);
    }
}
