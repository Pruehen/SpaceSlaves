using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Vector3 posDest = Vector3.zero;
    Vector3 posStart = Vector3.zero;
    Vector3 dir = Vector3.zero;

    float speedTendency = 1;
    // Start is called before the first frame update
    public void Move(Vector3 pos)
    {
        // 약간의 오차
        int randVal = 20;
        Vector3 addpos = new Vector3(
            Random.Range(-randVal, randVal),
            Random.Range(-randVal, randVal)
            );
        transform.position += addpos;

        // 전진
        posStart = transform.position;
        posDest = pos;
        dir = pos - posStart;
        dir = dir.normalized;
        Destroy(gameObject, 5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (posDest == Vector3.zero)
            return;
        var dist = Vector3.Distance(transform.position, posDest);
        if (dist < 5.0f)
        {
            Destroy(gameObject);
            return;
        }
        speedTendency += 65 * Time.deltaTime;
        transform.Translate(dir * 5 * speedTendency * Time.deltaTime, Space.Self);
    }
}
