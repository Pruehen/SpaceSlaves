using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeManager instance;
    


    [SerializeField]
    // "id / Ȱ��ȭ��"  ���� ������ ��ųʸ�
    Dictionary<string, bool> UpgradeActiveDict = new Dictionary<string, bool>();

    //check ����
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
