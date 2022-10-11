using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpownSystem : MonoBehaviour
{
    public void FriendlyShipSpown(int[] positioning)
    {
        for(int i = 0; i < positioning.Length; i++)
        {
            if (positioning[i] >= 0)
            {                
                int shipQty = FleetManager.instance.GetFleetQtyData(positioning[i]);
                for (int j = 0; j < shipQty; j++)
                {
                    GameObject ship = Instantiate(shipPrf[positioning[i]], new Vector3(j * 0.2f, 0, 0), Quaternion.identity, BattleSceneManager.instance.FriendlyManager);
                    ship.GetComponent<ShipControl>().idSet(positioning[i]);
                }
            }
        }        
    }

    public List<GameObject> shipPrf = new List<GameObject>();
}
