using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpownSystem : MonoBehaviour
{
    public void FriendlyShipSpown(int[] positioning)
    {
        for(int i = 0; i < positioning.Length; i++)
        {
            if(i >= 0)
            {                
                int shipQty = FleetManager.instance.GetFleetQtyData(positioning[i]);
                for (int j = 0; j < shipQty; j++)
                {
                    Instantiate(shipPrf[i], new Vector3(j * 0.1f, 0, 0), Quaternion.identity, BattleSceneManager.instance.FriendlyManager);
                }
            }
        }        
    }

    public List<GameObject> shipPrf = new List<GameObject>();
}
