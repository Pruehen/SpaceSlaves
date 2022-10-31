using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    // Start is called before the first frame update
    public void Move(Vector3 pos)
    {
        // 약간의 오차
        int randVal = 160;
        Vector3 addpos = new Vector3(
            Random.Range(-randVal, randVal),
            Random.Range(-randVal, randVal)
            );
        transform.position += addpos;

        // 전진
        Vector3 posDest = pos - transform.position;

        GetComponent<Rigidbody2D>().AddForce(posDest.normalized * 1530, ForceMode2D.Impulse);
        Destroy(gameObject, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
