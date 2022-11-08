using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipLock : MonoBehaviour
{
    public List<Image> Lock = new List<Image>();

    private void Start()
    {
        LockState();
    }

    void LockState()
    {
        for(int i = 0; i < Lock.Count; i++)
        {
            if (UpgradeManager.instance.GetFleetLevel() - 1 < i)
            {
                Lock[i].gameObject.SetActive(true);
            }
            else
            {
                Lock[i].gameObject.SetActive(false);
            }
        }   
    }
}
