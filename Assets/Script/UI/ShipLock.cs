using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipLock : MonoBehaviour
{
    public List<Image> Lock = new List<Image>();
    public List<Image> ShipImage = new List<Image>();

    public void LockState()
    {
        for(int i = 0; i < Lock.Count; i++)
        {
            Color color = ShipImage[i].color;

            if (UpgradeManager.instance.GetFleetLevel() - 1 < i)
            {
                Lock[i].gameObject.SetActive(true);
                color = new Color(0.78f, 0.15f, 0.15f, 1f);
                ShipImage[i].color = color;
            }
            else
            {
                Lock[i].gameObject.SetActive(false);

                color = Color.white;
                ShipImage[i].color = color;
            }
        }   
    }
}
