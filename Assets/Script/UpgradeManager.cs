using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeManager instance;
    


    [SerializeField]
    // "id / È°¼ºÈ­µÊ"  À¸·Î ±¸¼ºµÈ µñ¼Å³Ê¸®
    Dictionary<string, bool> UpgradeActiveDict = new Dictionary<string, bool>();

    //check Á¶°Ç
    public bool CheckUpgradeable(string id)
    {
        return true;
    }


    public void NewUpgrade()
    { 

    }

    private void Start()
    {
        
    }
}
