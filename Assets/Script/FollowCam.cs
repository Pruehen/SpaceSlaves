using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Camera FCam;
    public Transform Target;
    public List<GameObject> FoundTarget = new List<GameObject>();
    Transform tShip;
    bool TargetOn;

    private void Start()
    {
        FCam = GetComponent<Camera>();
    }

    void Update()
    {
        FollowSet();
        SetTarget();
    }

    void FollowSet()
    {
        for (int i = 0; i < Target.childCount; i++)
        {
            FoundTarget.Add(Target.GetChild(i).gameObject);
        }
    }

    void SetTarget()
    {
        IsFocus();

        if (TargetOn == true)
        {
            for (int i = 0; i < FoundTarget.Count; i++)
            {
                tShip = FoundTarget[i].transform;
                //FCam.transform.LookAt(tShip);
                FCam.transform.position = new Vector3(tShip.position.x + 0.2f, tShip.position.y + 0.2f, tShip.position.z + 0.2f);
                FCam.transform.rotation = tShip.rotation;
            }
        }
    }

    void IsFocus()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TargetOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            TargetOn = false;
        }
    }
}