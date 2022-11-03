using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTextManager : MonoBehaviour
{
    public GameObject dmgTmpPrf;
    public int tmpMax = 100;

    List<DmgTmp> tmpList = new List<DmgTmp>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < tmpMax; i++)
        {
            GameObject item = Instantiate(dmgTmpPrf, this.transform.GetChild(0).transform);

            tmpList.Add(item.GetComponent<DmgTmp>());
        }

    }

    int controledIndex = 0;
    public void SetDmgTmp(Transform targetTrf, int text, DmgTextType dmgTextType)
    {
        tmpList[controledIndex].Set(targetTrf, text, dmgTextType);
        controledIndex++;

        if(controledIndex >= tmpList.Count)
        {
            controledIndex = 0;
        }
    }
}
