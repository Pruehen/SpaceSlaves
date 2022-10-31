using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopEffects : MonoBehaviour
{

    public GameObject spr;
    int remainCnt = 0;
    List<GameObject> images = new List<GameObject>();
    float timer = 0;

    public void Pop(int cnt)
    {
        remainCnt = cnt;
    }
    public void Update()
    {        
        if (remainCnt <= 0)
            return;
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            return;
        }

        remainCnt--;

        Vector3 pos = FindObjectsOfType<CurrencyUI>()[0].transform.GetChild(0).gameObject.transform.position;
        GameObject go = Instantiate<GameObject>(spr, transform);
        Vector3 posDest = pos - transform.position;            

        go.GetComponent<Rigidbody2D>().AddForce(posDest.normalized * 550, ForceMode2D.Impulse);
        Destroy(go, 1.0f);

        timer = 0.05f;
    }
}
