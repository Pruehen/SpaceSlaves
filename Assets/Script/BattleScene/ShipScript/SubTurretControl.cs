using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTurretControl : MonoBehaviour
{
    public List<Turret> turrets = new List<Turret>();
    [SerializeField] int referenceId = 4;
    [SerializeField] float range = 30;

    public void GetAndSetData(int id)
    {
        ShipInfoData refData = FleetManager.instance.GetShipData(id);

        for (int i = 0; i < turrets.Count; i++)
        {
            turrets[i].TurretDataInit(refData.dmg, refData.dmgType, refData.fireDelay, range);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetAndSetData(referenceId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
