using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class FleetUnit
{
    public int id;
    public FleetUnit(int id)
    {
        this.id = id;
    }

}
[SerializeField]
public class FleetFormation
{
    public int idType;
    int size = 10;
    FleetUnit[] units;
    
    public void Add(int id, out bool isSuccess)
    {
        isSuccess = false;

        if (units.Length == 0)
            idType = id;

        if (units.Length > size)
            return;

        if (id != idType)
            return;

        var unit = new FleetUnit(id);
        units[units.Length] = unit;
        isSuccess = true;
    }
    public void Remove()
    {
        if (units.Length <= 0)
            return;
        units[units.Length - 1] = null;
    }
}

public class FleetFormationManager : MonoBehaviour
{
    [SerializeField]
    Dictionary<int, FleetFormation> formations;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool AddUnit(int id, int formIdx)
    {
        if (formations.ContainsKey(id))
        {
            formations[formIdx].Add(id, out bool success);
            return success;
        }

        var data = new FleetFormation();        
        data.Add(id, out bool suc);
        formations.Add(formIdx, data);
        return suc;
    }

    public bool RemoveUnit(int formIdx)
    {
        if (formations.ContainsKey(formIdx))
        {
            formations[formIdx].Remove();
            return true;
        }
        return false;
    }

}
